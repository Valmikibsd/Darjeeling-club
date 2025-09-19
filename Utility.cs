using Microsoft.AspNetCore.Mvc.Rendering;
using Nancy.Helpers;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace ClubApp
{
    public class db_Utility
    {
        ClsUtility clsutil = new ClsUtility();
        SqlCommand sqcmd;
        SqlDataAdapter da;
        //public string strElect = "Data Source=180.179.212.200,1533;Initial Catalog=ElectionCounting;Persist Security Info=True;User ID=assemblyelections; Password=El#ction@2021$%";
        //public string strElect1 = "Data Source=180.179.212.200,1533;Initial Catalog=ELECTIONRESULTS;Persist Security Info=True;User ID=assemblyelections; Password=El#ction@2021$%";
        //public string strLOCALElect1 = "Data Source=DESKTOP-ES7RBHJ\\SQLEXPRESS03;Initial Catalog=ELECTIONRESULT; Integrated Security=SSPI";

        //public string Clubstr = "Data Source=144.168.39.28,1633;Initial Catalog=club;Persist Security Info=True;User ID=club;Password=qpsdemtcikvnbjgol1zf";

      SqlConnection sqcon = new SqlConnection(@"Data Source=103.21.58.192;Initial Catalog=testdgcclub;Persist Security Info=True;User ID=testdgcclub;Password=uB33x7y%4");
      // SqlConnection sqcon = new SqlConnection(@"Data Source=103.21.58.192;Initial Catalog=dgcgym;Persist Security Info=True;User ID=dgcgym1;Password=~1FXvhq~f0T1albd");
        //public string Clubstr = "Data Source=103.21.58.192;Initial Catalog=dgcgym;Persist Security Info=True;User ID=dgcgym1;Password=~1FXvhq~f0T1albd";
       public string Clubstr = "Data Source=103.21.58.192;Initial Catalog=testdgcclub;Persist Security Info=True;User ID=testdgcclub;Password=uB33x7y%4";

        
        SqlDataAdapter SqlDa;
        internal object clubster;
        public string execQuery(string query, string constring = "")
        {
            clsutil.WriteLogFile("Apilog", "input'" + query + "'", "", "", "", "", "", "", "execQuery", "");
            using (SqlConnection sqcon = new SqlConnection(Clubstr))
            {


                SqlTransaction SqlTran = null;
                DataTable dt = new DataTable();
                try
                {
                    if (sqcon.State == ConnectionState.Open)
                    { sqcon.Close(); }
                    sqcon.Open();
                    SqlTran = sqcon.BeginTransaction();
                    SqlCommand sqcmd = new SqlCommand(query, sqcon, SqlTran);
                    sqcmd.CommandTimeout = 0;

                    SqlDataAdapter SqlDa = new SqlDataAdapter(sqcmd);
                    SqlDa.Fill(dt);
                    SqlTran.Commit();
                    if (dt.Rows.Count > 0)
                    {
                        query = dt.Rows[0]["Message"].ToString();
                    }
                    else
                    {
                        query = "Successfull";
                    }
                }

                catch (Exception exce)
                {

                    query = "Transaction Rolleutilck. Due to " + exce.Message;
                    clsutil.WriteLogFile("Errorlog", "OutPut'" + query + "--" + exce.Message + "'", "", "", "", "", "", "", "execQuery", "");
                }
                finally
                {
                    sqcon.Close();
                }
            }
            return query;
        }
     
        public DataTable SelectParticular(string tables, string ColName, string condition)
        {
            DataTable ResSet = new DataTable();
            using (SqlConnection sqcon = new SqlConnection(Clubstr))
            {


                try
                {

                    string query = "select " + ColName + " from " + tables + " where " + condition;
                    da = new SqlDataAdapter(query, sqcon);
                    da.Fill(ResSet);
                }
                catch (Exception ex)
                {
                    //PP'L;
                }
            }
            return ResSet;
        }


        public DataSet BindDropDown(string Query)
        {
            using (SqlConnection con = new SqlConnection(Clubstr))
            {
                SqlCommand com = new SqlCommand(Query, con);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }
        public DataSet TableBind(string query)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(Clubstr))
                {
                    sqlcon.Open();
                    SqlCommand cmd = new SqlCommand(query, sqlcon);
                    SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
                    sqlda.Fill(ds);
                    sqlcon.Close();
                }
            }
            catch (Exception exce)
            {

            }
            return ds;
        }

        public List<SelectListItem> PopulateDropDown(string Query, string constring, string select = "")
        {
            //int max = 0;
            DataTable dt = new DataTable();
            List<SelectListItem> ddl = new List<SelectListItem>();
            try
            {

                using (SqlConnection con = new SqlConnection(constring))
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        da.Fill(dt);
                }
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ddl.Add(new SelectListItem { Text = dt.Rows[i][1].ToString(), Value = dt.Rows[i][0].ToString() });
                    }
                }

                //max = Convert.ToInt32(dt.AsEnumerable().Max(row => row["id"]));
                //select = maximum;
                //if (select != "")
                //{
                //    var selddl = ddl.ToList().Where(x => x.Value = select).First();
                //    selddl.Selected = true;
                //}


            }
            catch (Exception ex)
            {
                //WriteLogFile("Errorlog", "input'" + Query + "---Output--" + ex.Message + "'", "", "", "", "", "", "", "Fill");
            }
            return ddl;
        }
        private static string[] units = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        private static string[] tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        public string NumberToWords(int number)
        {
            if (number == 0)
                return "Zero";

            if (number < 0)
                return "Minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                if (number < 20)
                    words += units[number];
                else
                {
                    words += tens[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + units[number % 10];
                }
            }

            return words;
        }
        public DataTable execQuery(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                //Writejson(sql + "" + sqcon);
                //WriteLogFile("Connection", "execQuery", "Check Connection");
                sqcmd = new SqlCommand(sql, sqcon);
                SqlDa = new SqlDataAdapter(sqcmd);
                SqlDa.Fill(dt);
            }
            catch (Exception exce)
            {
            }
            return dt;
        }
        public string BindDiv(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='data-table' class='table table-bordered table-hover w-100 dataTable dtr-inline'><thead><tr>");
            foreach (DataColumn column in dt.Columns)
            {
                sb.Append("<th class='thead-dark' style='color: #000000;background-color:#dadada;border-color:#9c9c9c;" + (column.ColumnName.Contains("Hid_") == true ? "display:none;" : "") + "'>" + column.ColumnName + "</th>");

            }
            sb.Append("</tr></thead>");
            foreach (DataRow row in dt.Rows)
            {
                sb.Append("<tr id='mytable'>");
                foreach (DataColumn column in dt.Columns)
                {
                    sb.Append("<td  style='width:100px;border: 1px solid #ccc;" + (column.ColumnName.Contains("Hid_") == true ? "display:none;" : "") + "'>" + row[column.ColumnName].ToString() + "</td>");

                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            return (sb.ToString());
        }
        public string MultipleTransactions(string query)
        {
            SqlTransaction SqlTran = null;
            try
            {
                sqcon.Open();
                SqlTran = sqcon.BeginTransaction();
                sqcmd = new SqlCommand(query, sqcon, SqlTran);
                sqcmd.ExecuteNonQuery();
                SqlTran.Commit();
                query = "Successfull";
            }
            catch (Exception exce)
            {
                SqlTran.Rollback();
                query = "Transaction Rolledback. Due to " + exce.Message;
            }
            finally
            {
                sqcon.Close();
            }
            return query;
        }
        public DataTable execQuerydt(string query, string constring = "")
        {
            DataTable dt = new DataTable();
        
            using (SqlConnection sqcon = new SqlConnection(Clubstr))
            {


                SqlTransaction SqlTran = null;
               
                try
                {
                    if (sqcon.State == ConnectionState.Open)
                    { sqcon.Close(); }
                    sqcon.Open();
                    SqlTran = sqcon.BeginTransaction();
                    SqlCommand sqcmd = new SqlCommand(query, sqcon, SqlTran);
                    sqcmd.CommandTimeout = 0;

                    SqlDataAdapter SqlDa = new SqlDataAdapter(sqcmd);
                    SqlDa.Fill(dt);
                    SqlTran.Commit();
                    if (dt.Rows.Count > 0)
                    {
                        query = dt.Rows[0]["Message"].ToString();
                    }
                    else
                    {
                        query = "Successfull";
                    }
                }

                catch (Exception exce)
                {

                    query = "Transaction Rolleutilck. Due to " + exce.Message;
                    clsutil.WriteLogFile("Errorlog", "OutPut'" + query + "--" + exce.Message + "'", "", "", "", "", "", "", "execQuery", "");
                }
                finally
                {
                    sqcon.Close();
                }
            }
            return dt;
        }
        public string RefNoUPDATE( int Branchid, int projectid, string controlcode, string date, string constring = "")
        {
            string code = "", prefix = "", Ref = "";
            using (SqlConnection sqcon = new SqlConnection(constring))
            {

                SqlTransaction SqlTran = null;
                
                if (sqcon.State == ConnectionState.Open)
                { sqcon.Close(); }
                sqcon.Open();
                SqlTran = sqcon.BeginTransaction();
                DataTable dt = new DataTable();

                sqcmd = new SqlCommand("Sp_IU_No_UPDATE", sqcon);
                sqcmd.CommandType = CommandType.StoredProcedure;
                sqcmd.Transaction = SqlTran;
                sqcmd.Parameters.Add(new SqlParameter("@BranchID", SqlDbType.Int)).Value = Branchid;
                sqcmd.Parameters.Add(new SqlParameter("@ProjectID", SqlDbType.Int)).Value = projectid;
                sqcmd.Parameters.Add(new SqlParameter("@ControlCode", SqlDbType.VarChar, 10)).Value = controlcode;
                sqcmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.VarChar, 20)).Value = date;
                da = new SqlDataAdapter(sqcmd);
                da.Fill(dt);

             

            }
            return Ref;
        }

        public string RefNobill( int Branchid, int projectid, string controlcode, string date, string constring = "")
        {
            string code = "", prefix = "", Ref = "";
            using (SqlConnection sqcon = new SqlConnection(constring))
            {

                SqlTransaction SqlTran = null;
                DataTable dt = new DataTable();
                if (sqcon.State == ConnectionState.Open)
                { sqcon.Close(); }
                sqcon.Open();
                SqlTran = sqcon.BeginTransaction();

                sqcmd = new SqlCommand("Sp_IU_No_NEW_SELECT", sqcon);
                sqcmd.CommandType = CommandType.StoredProcedure;
                sqcmd.Transaction = SqlTran;
                sqcmd.Parameters.Add(new SqlParameter("@BranchID", SqlDbType.Int)).Value = Branchid;
                sqcmd.Parameters.Add(new SqlParameter("@ProjectID", SqlDbType.Int)).Value = projectid;
                sqcmd.Parameters.Add(new SqlParameter("@ControlCode", SqlDbType.VarChar, 10)).Value = controlcode;
                sqcmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.VarChar, 20)).Value = date;
                da = new SqlDataAdapter(sqcmd);
                da.Fill(dt);

               
                code = dt.Rows[0][0].ToString();
                prefix = dt.Rows[0][1].ToString();
                if (code.Length == 1)
                {
                    Ref = prefix + "0000000" + code;
                }
                if (code.Length == 2)
                {
                    Ref = prefix + "000000" + code;
                }
                if (code.Length == 3)
                {
                    Ref = prefix + "00000" + code;
                }
                if (code.Length == 4)
                {
                    Ref = prefix + "0000" + code;
                }
                if (code.Length == 5)
                {
                    Ref = prefix + "000" + code;
                }
                if (code.Length == 6)
                {
                    Ref = prefix + "00" + code;
                }
                if (code.Length == 7)
                {
                    Ref = prefix + "0" + code;
                }

                if (code.Length == 8)
                {
                    Ref = prefix + code;
                }
            }
            return Ref;
        }
        public DataTable GetSingleTable(string strq, int cmdtimeout = 20, string constring = "")
        {
            clsutil.WriteLogFile("Apilog", "input'" + strq + "'", "", "", "", "", "", "", "execQuery", "");
            DataTable dt = new DataTable();
            try
            {

                using (SqlConnection con = new SqlConnection(constring))
                using (SqlCommand cmd = new SqlCommand(strq, con))
                {
                    cmd.CommandTimeout = cmdtimeout;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                clsutil.WriteLogFile("Errorlog", "OutPut'" + strq + "--" + ex.Message + "'", "", "", "", "", "", "", "GetSingleTable", "");
            }
            return dt;
        }

        public DataSet Fill(string sql, string constring)
        {
            DataSet ds = new DataSet();
            clsutil.WriteLogFile("Apilog", "input'" + sql + "'", "", "", "", "", "", "", "Fill", "");
            using (SqlConnection sqcon = new SqlConnection(constring))
            {
                try
                {
                    SqlCommand sqcmd = new SqlCommand(sql, sqcon);
                    sqcmd.CommandTimeout = 0;
                    SqlDataAdapter SqlDa = new SqlDataAdapter(sqcmd);
                    SqlDa.Fill(ds);
                }
                catch (Exception exce)
                {
                    clsutil.WriteLogFile("Errorlog", "OutPut'" + sql + "--" + exce.Message + "'", "", "", "", "", "", "", "Fill", "");
                }
            }
            return ds;
        }

        public DataSet CommonFill(string Data, string constring, int Userid)
        {

            clsutil.WriteLogFile("Apilog", "input'" + Data + "'", "", "", "", "", "", "", "'commonfill'", "");
            DataSet ds = new DataSet();
            using (SqlConnection sqcon = new SqlConnection(constring))
            {
                try
                {
                    SqlCommand sqcmd = new SqlCommand(Data, sqcon);
                    sqcmd.CommandTimeout = 0;
                    SqlDataAdapter SqlDa = new SqlDataAdapter(sqcmd);
                    SqlDa.Fill(ds);
                }
                catch (Exception exce)
                {
                    // clsutil.WriteLogFile("Errorlog", "OutPut'" + Querry + "--" + exce.Message + "'", "", "", "", "", "", "", "" + Apiname + "", "");
                }
            }
            return ds;
        }

       


       

        


    }

    public class ClsUtility
    {
        
      

        public void WriteLogFile(string LogPath, string Query, string Button, string Page, string IP, string BrowserName, string BrowerVersion, string javascript, string function, string Userid)
        {
            try
            {

                if (!string.IsNullOrEmpty(Query))
                {
                    string path = Path.Combine("wwwroot/" + LogPath + "/" + System.DateTime.UtcNow.ToString("dd-MM-yyyy") + ".txt");

                    if (!File.Exists(path))
                    {
                        File.Create(path).Dispose();

                        using (System.IO.FileStream file = new FileStream(path, FileMode.Append, FileAccess.Write))
                        {

                            StreamWriter streamWriter = new StreamWriter(file);

                            streamWriter.WriteLine((((((((System.DateTime.Now + " - ") + Query + " - ") + Button + " - ") + Page + " - ") + IP + " - ") + BrowserName + " - ") + BrowerVersion + " - ") + javascript + function + "-" + "");

                            streamWriter.Close();

                        }
                    }
                    else
                    {
                        using (System.IO.FileStream file = new FileStream(path, FileMode.Append, FileAccess.Write))
                        {

                            StreamWriter streamWriter = new StreamWriter(file);

                            streamWriter.WriteLine((((((((System.DateTime.Now + " - ") + Query + " - ") + Button + " - ") + Page + " - ") + IP + " - ") + BrowserName + " - ") + BrowerVersion + " - ") + javascript + function);

                            streamWriter.Close();

                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }



        public string SendMailViaIIS_html(string from, string to, string cc, string bcc, string subject, string _body, IConfiguration iConfig, string MAIL_PASSWORD, string Host, string attachPath = "")
        {
            //create the mail message
            string functionReturnValue = null;
            string _from = from, _to = to, _cc = cc, _bcc = bcc, _subject = subject; //MAIL_PASSWORD = "15M7Y1998@$";
                                                                                     // _to = "ajay@bsdinfotech.com";

            _from = "info@darjeelinggymkhanaclub.in";
            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                //set the addresses
                if (_from.Trim().Length == 0)
                {
                    _from = "ajay@bsdinfotech.com;";
                    //_from = """Support Team"" support@indiastat.com"
                }
                mail.From = new System.Net.Mail.MailAddress("info@darjeelinggymkhanaclub.in");

                if (_to.Trim().Length > 0)
                {
                    mail.To.Add(new System.Net.Mail.MailAddress(_to));
                }
                if (_cc.Trim().Length > 0)
                {
                    mail.CC.Add(new System.Net.Mail.MailAddress(_cc));
                }
                if (bcc.Trim().Length > 0 & bcc.Trim() != "none")
                {
                    mail.Bcc.Add(new System.Net.Mail.MailAddress(_bcc));
                }
                else if (bcc.Trim().Length == 0 & bcc.Trim() != "none")
                {
                    //mail.Bcc.Add(New system.net.mail.mailaddress("support@indiastat.com"))
                    //mail.Bcc.Add(New system.net.mail.mailaddress("diplnd07@gmail.com"))
                }

                if (!string.IsNullOrEmpty(attachPath))
                {
                    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(attachPath);
                    //create the attachment
                    mail.Attachments.Add(attachment);
                    //add the attachment
                }
                mail.Subject = _subject;
                mail.Body = _body;
                mail.IsBodyHtml = true;
                System.Net.Mail.SmtpClient SmtpClient = new System.Net.Mail.SmtpClient();
                //SmtpClient.Host = iConfig.GetSection("ISSMTPSERVER").Value;
                //SmtpClient.Port = Convert.ToInt32(iConfig.GetSection("ISSMTPPORT").Value);
                SmtpClient.Host = "webmail.darjeelinggymkhanaclub.in";//"mail.bsdinfotech.com";
                SmtpClient.Credentials = new NetworkCredential("info@darjeelinggymkhanaclub.in", "k20@aoA60");
                SmtpClient.Port = 587;
              // SmtpClient.EnableSsl = true;
                SmtpClient.Send(mail);
                functionReturnValue = "Sent";
                mail.Dispose();
                SmtpClient = null;
            }
            catch (System.FormatException ex)
            {
                functionReturnValue = ex.Message;
            }
            catch (SmtpException ex)
            {
                functionReturnValue = ex.Message;
            }
            catch (System.Exception ex)
            {
                functionReturnValue = ex.Message;
            }
            return functionReturnValue;
        }
        public string SendMailViaIIS_htmlClub(string from, string to, string cc, string bcc, string subject, string _body, IConfiguration iConfig, string MAIL_PASSWORD, string Host, string attachPath = "")
        {
            //create the mail message
            string functionReturnValue = null;
            string _from = from, _to = to, _cc = cc, _bcc = bcc, _subject = subject; //MAIL_PASSWORD = "15M7Y1998@$";
            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                //set the addresses
                if (_from.Trim().Length == 0)
                {
                    _from = "sonu@bsdinfotech.com";
                    //_from = """Support Team"" support@indiastat.com"
                }
                mail.From = new System.Net.Mail.MailAddress("sonu@bsdinfotech.com");

                if (_to.Trim().Length > 0)
                {
                    mail.To.Add(new System.Net.Mail.MailAddress(_to));
                }
                if (_cc.Trim().Length > 0)
                {
                    mail.CC.Add(new System.Net.Mail.MailAddress(_cc));
                }
                if (bcc.Trim().Length > 0 & bcc.Trim() != "none")
                {
                    mail.Bcc.Add(new System.Net.Mail.MailAddress(_bcc));
                }
                else if (bcc.Trim().Length == 0 & bcc.Trim() != "none")
                {
                    //mail.Bcc.Add(New system.net.mail.mailaddress("support@indiastat.com"))
                    //mail.Bcc.Add(New system.net.mail.mailaddress("diplnd07@gmail.com"))
                }

                if (!string.IsNullOrEmpty(attachPath))
                {
                    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(attachPath);
                    //create the attachment
                    mail.Attachments.Add(attachment);
                    //add the attachment
                }
                mail.Subject = _subject;
                mail.Body = _body;
                mail.IsBodyHtml = true;
                System.Net.Mail.SmtpClient SmtpClient = new System.Net.Mail.SmtpClient();
                //SmtpClient.Host = iConfig.GetSection("ISSMTPSERVER").Value;
                //SmtpClient.Port = Convert.ToInt32(iConfig.GetSection("ISSMTPPORT").Value);
                // client.EnableSsl = false;
                SmtpClient.Host = "mail.bsdinfotech.com";//"mail.bsdinfotech.com";
                SmtpClient.Credentials = new NetworkCredential("sonu@bsdinfotech.com", "SLpsP@2025#Meta");
                SmtpClient.Port = 587;
                SmtpClient.EnableSsl = false;
                SmtpClient.Send(mail);
                functionReturnValue = "Sent";
                mail.Dispose();
                SmtpClient = null;
            }
            catch (System.FormatException ex)
            {
                functionReturnValue = ex.Message;
            }
            catch (SmtpException ex)
            {
                functionReturnValue = ex.Message;
            }
            catch (System.Exception ex)
            {
                functionReturnValue = ex.Message;
            }
            return functionReturnValue;
        }


        public String Encrypt(String plainText, String passphrase, String salt, Byte[] iv, int iterations)
        {
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            string data = ByteArrayToHexString(Encrypt(plainBytes, GetSymmetricAlgorithm(passphrase, salt, iv, iterations))).ToUpper();


            return data;
        }
        public String decrypt(String plainText, String passphrase, String salt, Byte[] iv, int iterations)
        {
            byte[] str = HexStringToByte(plainText);

            string data1 = Encoding.UTF8.GetString(decrypt(str, GetSymmetricAlgorithm(passphrase, salt, iv, iterations)));
            return data1;
        }
        public byte[] Encrypt(byte[] plainBytes, SymmetricAlgorithm sa)
        {
            return sa.CreateEncryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length);

        }
        public byte[] decrypt(byte[] plainBytes, SymmetricAlgorithm sa)
        {
            return sa.CreateDecryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }
        public SymmetricAlgorithm GetSymmetricAlgorithm(String passphrase, String salt, Byte[] iv, int iterations)
        {
            var saltBytes = new byte[16];
            var ivBytes = new byte[16];
            Rfc2898DeriveBytes rfcdb = new Rfc2898DeriveBytes(passphrase, Encoding.UTF8.GetBytes(salt), iterations, HashAlgorithmName.SHA512);
            saltBytes = rfcdb.GetBytes(32);
            var tempBytes = iv;
            Array.Copy(tempBytes, ivBytes, Math.Min(ivBytes.Length, tempBytes.Length));
            var rij = new RijndaelManaged(); //SymmetricAlgorithm.Create();
            rij.Mode = CipherMode.CBC;
            rij.Padding = PaddingMode.PKCS7;
            rij.FeedbackSize = 128;
            rij.KeySize = 128;

            rij.BlockSize = 128;
            rij.Key = saltBytes;
            rij.IV = ivBytes;
            return rij;
        }
        protected static byte[] HexStringToByte(string hexString)
        {
            try
            {
                int bytesCount = (hexString.Length) / 2;
                byte[] bytes = new byte[bytesCount];
                for (int x = 0; x < bytesCount; ++x)
                {
                    bytes[x] = Convert.ToByte(hexString.Substring(x * 2, 2), 16);
                }
                return bytes;
            }
            catch
            {
                throw;
            }
        }
       
        public static string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }



        public async Task<string> SendWhatsAppMessageAsync(string phoneNumber, string message, string instanceId, string accessToken)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // Construct URL (encode message to handle special chars like spaces, &, etc.)
                    string encodedMessage = Uri.EscapeDataString(message);
                    string url = $"https://hisocial.in/api/send?number={phoneNumber}&type=text&message={encodedMessage}&instance_id={instanceId}&access_token={accessToken}";

                    HttpResponseMessage response = await client.GetAsync(url);
                    string responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        // Parse JSON response (assume format like {"status": "success", "message": "Sent"}
                        dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
                        return $"WhatsApp sent successfully: {jsonResponse?.status ?? "OK"}";
                    }
                    else
                    {
                        return $"WhatsApp error: {response.StatusCode} - {responseContent}";
                    }
                }
                catch (Exception ex)
                {
                    return $"WhatsApp exception: {ex.Message}";
                }
            }
        }


        public async Task<string> SendWhatsAppWithAttachmentAsyc(string phoneNumber, string message, string instanceId, string accessToken, string filePath)
        {
            using (var client = new HttpClient())
            using (var formContent = new MultipartFormDataContent())
            {
                try
                {
                    // Add parameters
                    formContent.Add(new StringContent(phoneNumber), "number");
                    formContent.Add(new StringContent("document"), "type"); // Or "image" for images
                    formContent.Add(new StringContent(message), "message"); // Caption for attachment
                    formContent.Add(new StringContent(instanceId), "instance_id");
                    formContent.Add(new StringContent(accessToken), "access_token");

                    // Add file attachment
                    if (File.Exists(filePath))
                    {
                        var fileContent = new ByteArrayContent(File.ReadAllBytes(filePath));
                        fileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/octet-stream"); // Or "image/jpeg" for images
                        formContent.Add(fileContent, "attachment", Path.GetFileName(filePath)); // "attachment" is a common param name; check API docs
                    }
                    else
                    {
                        return "File not found for attachment.";
                    }

                    HttpResponseMessage response = await client.PostAsync("https://hisocial.in/api/send", formContent);
                    string responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
                        return $"WhatsApp with attachment sent: {jsonResponse?.status ?? "OK"}";
                    }
                    else
                    {
                        return $"WhatsApp attachment error: {response.StatusCode} - {responseContent}";
                    }
                }
                catch (Exception ex)
                {
                    return $"WhatsApp attachment exception: {ex.Message}";
                }
            }
        }


        public  async Task<string> SendWhatsAppWithAttachmentAsync(
         string phoneNumber, string message, string instanceId, string accessToken, string fileUrl)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string baseUrl = "https://hisocial.in/api/send";

                    // Encode message
                    string encodedMessage = HttpUtility.UrlEncode(message);

                    string url = $"{baseUrl}?number={phoneNumber}" +
                                 $"&type=document" +                 // use "document" for PDF/doc, "image" for image
                                 $"&message={encodedMessage}" +
                                 $"&media_url={fileUrl}" +           // <<==== Must be a URL, not file upload
                                 $"&instance_id={instanceId}" +
                                 $"&access_token={accessToken}";

                    HttpResponseMessage response = await client.GetAsync(url);
                    string result = await response.Content.ReadAsStringAsync();

                    return response.IsSuccessStatusCode
                        ? $"✅ Sent Successfully: {result}"
                        : $"❌ Failed: {response.StatusCode} - {result}";
                }
                catch (Exception ex)
                {
                    return $"⚠️ Exception: {ex.Message}";
                }
            }
        }





    }
}
