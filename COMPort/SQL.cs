using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace COMPort
{
    class SQL
    {
        public static string ServName { get; set; }
        public static string DBName { get; set; }
        //строка подключения к базам SQL
        private static string conString
        {
            get
            {
                return String.Format("Data Source=.\\{0};Integrated Security=SSPI", ServName);
            }
        }

        /// <summary>
        /// Прописывает все теги в базу UDM
        /// </summary>
        public static void UpdateUDMItems(int devSeq, Dictionary<string, float[]> devValues)
        {
            using (SqlConnection con = new SqlConnection(conString + ";Initial Catalog=" + DBName))
            {
                try
                {
                    con.Open();
                }
                catch(Exception ex)
                {
                    throw new Exception("Ошибка подключения к серверу БД: " + ex.Message);
                }
                foreach (string TKey in devValues.Keys)
                {
                    switch (TKey)
                    {
                        case "Online":
                            WriteToUDM(con,TKey, devSeq, devValues[TKey][0].ToString());
                            break;
                        case "P":
                            WriteToUDM(con, "Ptotal", devSeq, devValues[TKey][0].ToString());
                            WriteToUDM(con, "PL1", devSeq, devValues[TKey][1].ToString());
                            WriteToUDM(con, "PL2", devSeq, devValues[TKey][2].ToString());
                            WriteToUDM(con, "PL3", devSeq, devValues[TKey][3].ToString());
                            break;
                        case "Q":
                            WriteToUDM(con, "Qtotal", devSeq, devValues[TKey][0].ToString());
                            WriteToUDM(con, "QL1", devSeq, devValues[TKey][1].ToString());
                            WriteToUDM(con, "QL2", devSeq, devValues[TKey][2].ToString());
                            WriteToUDM(con, "QL3", devSeq, devValues[TKey][3].ToString());
                            break;
                        case "S":
                            WriteToUDM(con, "Stotal", devSeq, devValues[TKey][0].ToString());
                            WriteToUDM(con, "SL1", devSeq, devValues[TKey][1].ToString());
                            WriteToUDM(con, "SL2", devSeq, devValues[TKey][2].ToString());
                            WriteToUDM(con, "SL3", devSeq, devValues[TKey][3].ToString());
                            break;
                        case "CosPh":
                            WriteToUDM(con, "CosPhi", devSeq, devValues[TKey][0].ToString());
                            WriteToUDM(con, "CosPhiL1", devSeq, devValues[TKey][1].ToString());
                            WriteToUDM(con, "CosPhiL2", devSeq, devValues[TKey][2].ToString());
                            WriteToUDM(con, "CosPhiL3", devSeq, devValues[TKey][3].ToString());
                            break;
                        case "I":
                            WriteToUDM(con, "IL1", devSeq, devValues[TKey][1].ToString());
                            WriteToUDM(con, "IL2", devSeq, devValues[TKey][2].ToString());
                            WriteToUDM(con, "IL3", devSeq, devValues[TKey][3].ToString());
                            break;
                        case "Uf":
                            WriteToUDM(con, "UL1N", devSeq, devValues[TKey][1].ToString());
                            WriteToUDM(con, "UL2N", devSeq, devValues[TKey][2].ToString());
                            WriteToUDM(con, "UL3N", devSeq, devValues[TKey][3].ToString());
                            break;
                        case "Kuf":
                            WriteToUDM(con, "KufL1N", devSeq, devValues[TKey][1].ToString());
                            WriteToUDM(con, "KufL2N", devSeq, devValues[TKey][2].ToString());
                            WriteToUDM(con, "KufL3N", devSeq, devValues[TKey][3].ToString());
                            break;
                        case "Umf":
                            WriteToUDM(con, "UL12", devSeq, devValues[TKey][1].ToString());
                            WriteToUDM(con, "UL23", devSeq, devValues[TKey][2].ToString());
                            WriteToUDM(con, "UL31", devSeq, devValues[TKey][3].ToString());
                            break;
                        case "Kumf":
                            WriteToUDM(con, "KumfL12", devSeq, devValues[TKey][1].ToString());
                            WriteToUDM(con, "KumfL23", devSeq, devValues[TKey][2].ToString());
                            WriteToUDM(con, "KumfL31", devSeq, devValues[TKey][3].ToString());
                            break;
                        case "U1_1":
                            WriteToUDM(con, "U1_1", devSeq, devValues[TKey][0].ToString());
                            break;
                        case "K0u":
                            WriteToUDM(con, "K0u", devSeq, devValues[TKey][0].ToString());
                            break;
                        case "K2u":
                            WriteToUDM(con, "K2u", devSeq, devValues[TKey][0].ToString());
                            break;
                        case "F":
                            WriteToUDM(con, "Fs", devSeq, devValues[TKey][0].ToString());
                            break;
                        case "T":
                            WriteToUDM(con, "Tins", devSeq, devValues[TKey][0].ToString());
                            break;
                    }
                }
                con.Close();
            }
        }

        /// <summary>
        /// Прописывает тег в базу UDM
        /// </summary>
        /// <param name="con">активное подключение SQLConnection</param>
        /// <param name="param">имя параметра</param>
        /// /// <param name="devSeq">номер устройства</param>
        /// <param name="value">значение параметра</param>
        private static void WriteToUDM(SqlConnection con,string param, int devSeq, string value)
        {
            try
            {
                const string comText =
                    "UPDATE [dbo].[DMG_ExpressionItems] SET [ReadExpression] = @Value WHERE [ID] = @ID";
                SqlCommand command = new SqlCommand(comText, con);
                command.Parameters.Add(new SqlParameter("Value", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("ID", SqlDbType.Int));

                string id =
                    GetUDMItemId(con,
                        GetUDMParId(con,
                            GetUDMParId(con,
                                GetUDMParId(con, null, "AI", 1),
                            "SET" + devSeq, 1),
                        param, 1),
                    "Val", 1);

                if (id == null)
                    return;
                command.Parameters["Value"].Value = value.Replace(",", ".");
                command.Parameters["ID"].Value = id;

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось обновить базу! \n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// Получает значение ID заданной папки
        /// </summary>
        /// <param name="con">активное подключение SQLConnection</param>
        /// <param name="recParId">значение столбца RecursiveParentID</param>
        /// <param name="name">значение столбца Name</param>
        /// <param name="table">0-DMG_RegisterFolders; 1-DMG_ExpressionFolders</param>
        /// <returns>ID в формате string, null если параметр не найден или в случае ошибки выполнения</returns>
        private static string GetUDMParId(SqlConnection con, string recParId, string name, int table)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand selectCommand =
                    table == 0
                        ? new SqlCommand("SELECT [ID],[RecursiveParentID],[Name] FROM [dbo].[DMG_RegisterFolders]", con)
                        : table == 1
                            ? new SqlCommand("SELECT [ID],[RecursiveParentID],[Name] FROM [dbo].[DMG_ExpressionFolders]",
                                con)
                            : null;
                SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Name"].ToString() != name) continue;
                    if (dt.Rows[i]["RecursiveParentID"].ToString() ==
                        (string.IsNullOrEmpty(recParId) ? string.Empty : recParId))
                        return dt.Rows[i]["ID"].ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(string.Format(
                //    "Метод: GetParId(SqlConnection con, string recParId, string name, int table) \n\nТекст ошибки: {0} \n\nИмя параметра: {1}",
                //    ex.Message, name), "Не удалось создать SqlCommand");
                return null;
            }
        }

        /// <summary>
        /// Получает значение ID указанного параметра (из базы UDM)
        /// </summary>
        /// <param name="con">активное подключение SQLConnection</param>
        /// <param name="parId">значение столбца ParentID</param>
        /// <param name="name">значение столбца Name</param>
        /// <param name="table">0-DMG_RegisterItems; 1-DMG_ExpressionItems</param>
        /// <returns>ID в формате string, null если параметр не найден и в случае ошибки выполнения</returns>
        private static string GetUDMItemId(SqlConnection con, string parId, string name, int table)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand selectCommand =
                    table == 0
                        ? new SqlCommand("SELECT [ID],[ParentID],[Name] FROM [dbo].[DMG_RegisterItems]", con)
                        : table == 1
                            ? new SqlCommand("SELECT [ID],[ParentID],[Name] FROM [dbo].[DMG_ExpressionItems]",
                                con)
                            : null;
                SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Name"].ToString() != name) continue;
                    if (dt.Rows[i]["ParentID"].ToString() ==
                        (string.IsNullOrEmpty(parId) ? string.Empty : parId))
                        return dt.Rows[i]["ID"].ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(string.Format(
                //    "Метод: GetUDMItemId(SqlConnection con, string parId, string name, int table) \n\nТекст ошибки: {0} \n\nИмя параметра: {1}",
                //    ex.Message, name), "Не удалось получить ID из таблицы dbo.DMG_RegisterItems\\dbo.DMG_ExpressionItems");
                return null;
            }
        }
    }
}
