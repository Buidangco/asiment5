using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleApp1
{
    public class Outlookmail
    {
        public string From { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        public string Subject { get; set; }
        public string Attachment { get; set; }
        public string Mailbody { get; set; }
        public Boolean IsImportant { get; set; }
        public string Password { get; set; }
        public Boolean Sent { get; set; }


        //Ham check mail 
        public static bool isEmail(string inputEmail)
        {
            inputEmail = inputEmail ?? string.Empty;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }
        //Nhap du lieu dau mail
        public void nhap()
        {
            Console.WriteLine("Input thong tin mail");
#pragma warning disable CS0168 // Variable is declared but never used
            int n;
#pragma warning restore CS0168 // Variable is declared but never used
            do
            {
                //Input From
                do
                {
                    Console.WriteLine();
                    Console.Write("\tFrom :");
                    From = Console.ReadLine().Trim();
                    if (isEmail(From))
                    {
                        this.From = From;
                    }
                    else
                    {
                        throw new Exception("Invalid email");
                    }

                } while (false);
                //Input To
                do
                {
                    Console.Write("\n\tTo :");
                    To = Console.ReadLine();
                    string[] subs = To.Split(',');
                    this.To = "";
                    foreach (var sub in subs)
                    {
                        if (isEmail(sub.Trim()))
                        {
                            this.To += sub + ",";
                        }
                        else
                        {
                            throw new Exception("Invalid email");
                        }
                    }
                    this.To = To.Substring(0, To.Length - 1);
                } while (false);
                //Input CC
                do
                {
                    Console.Write("\n\tCC :");
                    CC = Console.ReadLine().Trim();
                    string[] subs = CC.Split(',');
                    this.CC = "";
                    foreach (var sub in subs)
                    {
                        if (isEmail(sub.Trim()))
                        {
                            this.CC += sub + ",";
                        }
                        else
                        {
                            throw new Exception("Invalid email");
                        }
                    }
                    this.CC = CC.Substring(0, CC.Length - 1);
                } while (false);

#pragma warning disable CS0219 // Variable is assigned but its value is never used
                string c = @"";
#pragma warning restore CS0219 // Variable is assigned but its value is never used
                List<string> lst = new List<string>();
                Console.Write("\n\tSubject :");
                Subject = Console.ReadLine();
                Console.Write("\n\tAttachment :");
                Attachment = Console.ReadLine();

                //Input Mail Body
                Console.WriteLine("\n\tMail Body(Input END de ket thuc nhap) :");
                do
                {
                    Mailbody = Console.ReadLine();

                    if (Mailbody != "END")
                    {
                        lst.Add(Mailbody);
                    }
                    else
                    {
                        this.Mailbody = "";
                        foreach (var i in lst)
                        {
                            if (i != "END")
                            {
                                this.Mailbody += i + "\n";
                            }
                        }
                        break;
                    }
                } while (true);

                Console.Write("\n\tIs Improtant(true/false) :");
                IsImportant = Boolean.Parse(Console.ReadLine());
                Console.Write("\n\tPassword :");
                Password = Console.ReadLine();
                this.Password = EncodePasswordToBase64(Password);
                Console.Write("\n\tNhap(True/False) cho Send\n\t Neu true luu thong tin\n\tNeu false nhap lai tu bat dau\n\t Moi nhap send(true/false) :");
                Sent = Boolean.Parse(Console.ReadLine());
            } while (Sent != true);
        }
        //this function Convert to Encord your Password 
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        } //this function Convert to Decord your Password
        public string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        public void display()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(@"Exercise 4 : Sort email list according to Subject field
Display the imported email list: From, To, Subject");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("From\t     To\t      CC\t     subject\t");
        }

        public void displayxml()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(@"Exercise 6 :Allow to enter the mail.xml file path
Read the file and display the email list.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("From\t     To\t      CC\t     subject\t Attachment\t   Body\t  IsImporttant\t  Password\t send\t");
        }
        public void hien(string From, string Too, string CC, string Subject)
        {
            Console.WriteLine("{0}\t{1}\t{2}\t{3}", From, Too, CC, Subject);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            DateTime date = DateTime.Now;
            Outlookmail a;
            List<Outlookmail> outlookmails = new List<Outlookmail>();
            while (true)
            {
                a = new Outlookmail();
                // nhập đối tượng Outlookmail
                a.nhap();
                //thêm vào list đối tượng
                outlookmails.Add(a);
              // hỏi xem có nhập nữa hay không
                Console.Write("\n\tDo you want to enter again(yes/no) :");
                string ys = Console.ReadLine();
                if (ys.Contains("yes"))
                {
                   
                    continue;
                }
                else break;
            }
            
            a.display();
            //4. Exercise 4 Xap xep theo subject
            foreach (var ay in outlookmails.OrderBy(t => t.Subject))
            {
                Console.WriteLine();
                a.hien(ay.From, ay.To, ay.CC, ay.Subject);
            }

            //Exercise 5 Save the email list to the mail.xml file with the following format:
            using (XmlWriter writer = XmlWriter.Create(@"D:\email.xml"))
            {
                writer.WriteStartElement("OutlookEmail");
                foreach (var ieltsTest in outlookmails)
                {
                    writer.WriteStartElement("Mail");
                    writer.WriteElementString("From", ieltsTest.From);
                    writer.WriteStartElement("To");
                    string[] to = ieltsTest.To.Split(',');
                    foreach (var i in to)
                    {
                        writer.WriteElementString("Address", i);
                    }
                    writer.WriteEndElement();
                    writer.Flush();
                    writer.WriteStartElement("CC");
                    string[] CC = ieltsTest.CC.Split(',');
                    foreach (var i in CC)
                    {
                        writer.WriteElementString("Address", i);
                    }
                    writer.WriteEndElement();
                    writer.Flush();
                    writer.WriteElementString("Subject", ieltsTest.Subject);
                    writer.WriteElementString("Attachment", ieltsTest.Attachment);
                    writer.WriteElementString("Body", ieltsTest.Mailbody);
                    writer.WriteElementString("IsImportant", ieltsTest.IsImportant.ToString());
                    writer.WriteElementString("Password", a.DecodeFrom64(ieltsTest.Password));
                    writer.WriteElementString("Sent", date.ToString());
                    writer.WriteEndElement();
                    writer.Flush();
                }
                writer.Flush();
            }

            //string filewirt = string.Concat(path.Substring(0, path.LastIndexOf('/')), "/ielts.xml");
            //Console.WriteLine(filewirt);
            ////Ghi thông tin ielts vào file ielts.xml
            //using (XmlWriter writer = XmlWriter.Create(@"" + filewirt))
            //    Console.Write("Nhap path file xml:");
            //string path = Console.ReadLine();
            a.displayxml();

            //Exercise 6 Allow to enter the mail.xml file path Read the file and display the email list.
                        XDocument xdoc = XDocument.Load(@"D:\email.xml");

            //đọc file từ file xml và lưu danh sách vào đối tượng khách hàng
            xdoc.Descendants("Mail").Select(p => new
            {
                from = p.Element("From").Value,
                To = p.Descendants("To").Descendants("Address").ToList(),
                CC = p.Descendants("CC").Descendants("Address").ToList(),
                Subject= p.Element("Subject").Value,
                Attachment = p.Element("Attachment").Value,
                Body = p.Element("Body").Value,
                IsImportant = p.Element("IsImportant").Value,
                Password = p.Element("Password").Value,
                Sent = p.Element("Sent").Value,
            }).ToList().ForEach(p =>
            {
                string c = "";
                string d = "";
                foreach (var i in p.To)
                {
                  
                    c += i.Value+",";
                }
                foreach (var j in p.CC)
                {
                    d += j.Value+",";
                }
                c=c.Substring(0, c.Length - 1);
                d=d.Substring(0, d.Length - 1);
                Console.WriteLine();
                Console.Write("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}", p.from,c,d, p.Subject, p.Attachment, p.Body, p.IsImportant, p.Password, p.Sent);
            });
            Console.ReadKey();
        }
    }
}
