using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Стамотология.Classes;
using System.IO;
using Microsoft.Office.Interop.Word;
using System.Runtime.Serialization.Formatters.Binary;

//ссылка на библиотеку
using DantistLibrary;

namespace Стамотология
{
    public partial class FormReestrPrint : Form
    {
        private bool реестрЗаключенныхДоговоров;
        private string льготнаяКатегорияТекст = string.Empty;

        private string queryReestr = string.Empty;

        //флаг хранит настройку разрешающую или запрещающую выгрузку реестра в файл
        private bool flagUnLoad = false;

        /// <summary>
        /// Указывает что форма работает в режиме реестра заключенных договоров
        /// </summary>
        public bool РеестрЗаключенныхДоговоров
        {
            get
            {
                return реестрЗаключенныхДоговоров;
            }
            set
            {
                реестрЗаключенныхДоговоров = value;
            }
        }
                

        public FormReestrPrint()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbЛьготнаяКатегория_Layout(object sender, LayoutEventArgs e)
        {
            //string sCon = ConnectionDB.ConnectionString();
            //string queryЛК = "select * from ЛьготнаяКатегория";
            //DataTable dtЛК = ТаблицаБД.GetTable(queryЛК, sCon, "ЛьготнаяКатегория");

            //this.cmbЛьготнаяКатегория.DataSource = dtЛК;
            //this.cmbЛьготнаяКатегория.DisplayMember = "ЛьготнаяКатегория";

        }

        private void button1_Click(object sender, EventArgs e)
        {

            //запишем название льготной категории
            льготнаяКатегорияТекст = this.cmbЛьготнаяКатегория.Text.Trim();

            int iCountЛьготник = Convert.ToInt32(ТаблицаБД.GetTable("select Count(*) from Льготник", ConnectionDB.ConnectionString(), "Льготник").Rows[0][0]);

            if (iCountЛьготник == 0)
            {
                MessageBox.Show("Нет данных по льготнику");
                this.Close();
                return;
            }

            
            string sCon = ConnectionDB.ConnectionString();
            this.dtEnd.Value = this.dtEnd.Value.AddDays(1);
          //  this.dtStart.Value = this.dtStart.Value.AddDays(-1);


            string endYear = this.dtEnd.Value.Year.ToString();
            //отними 1
            //string endDay = this.dtEnd.Value.Day.ToString();
            string endDay = Convert.ToString(this.dtEnd.Value.Day);
            string endMonth = this.dtEnd.Value.Month.ToString();

            string strYear = this.dtStart.Value.Year.ToString();
            //прибавим 1
            //string strDay = this.dtStart.Value.Day.ToString();
            string strDay =  Convert.ToString(this.dtStart.Value.Day);
            string strMonth = this.dtStart.Value.Month.ToString();

            string endДата = "#" + endMonth + "/" + endDay + "/" + endYear + "#";
            string strДата = "#" + strMonth + "/" + strDay + "/" + strYear + "#";
            this.dtEnd.Value = this.dtEnd.Value.AddDays(-1);

            //#02/05/2013# 

            //======================================Рабочая версия реест выполненных договоров
            //string queryReestr = "SELECT ЛьготнаяКатегория.ЛьготнаяКатегория as 'Льготная категория', Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, Договор.ДатаДоговора, Льготник.СерияДокумента, Льготник.НомерДокумента, Льготник.ДатаВыдачиДокумента,Sum(АктВыполнненныхРабот.Сумма) AS [Сумма], АктВыполнненныхРабот.НомерАкта, Договор.НомерДоговора, АктВыполнненныхРабот.ДатаПодписания " +
            //         "FROM (ЛьготнаяКатегория INNER JOIN Льготник ON ЛьготнаяКатегория.id_льготнойКатегории = Льготник.id_льготнойКатегории) INNER JOIN (Договор INNER JOIN АктВыполнненныхРабот ON Договор.id_договор = АктВыполнненныхРабот.id_договор) ON Льготник.id_льготник = Договор.id_льготник " +
            //         "WHERE (((ЛьготнаяКатегория.ЛьготнаяКатегория)='" + this.cmbЛьготнаяКатегория.Text + "')AND (Договор.ФлагНаличияАкта = True)) " +
            //         "GROUP BY ЛьготнаяКатегория.ЛьготнаяКатегория, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, Договор.ДатаДоговора, Льготник.СерияДокумента, Льготник.НомерДокумента, АктВыполнненныхРабот.НомерАкта, Договор.НомерДоговора, АктВыполнненныхРабот.ДатаПодписания, АктВыполнненныхРабот.ДатаПодписания, Льготник.ДатаВыдачиДокумента " +
            //         "HAVING (((АктВыполнненныхРабот.ДатаПодписания)>= " + strДата + " And (АктВыполнненныхРабот.ДатаПодписания)< " + endДата + "))";


            //======================================Рабочая версия реестра с суммами в договорах==============
            if (this.РеестрЗаключенныхДоговоров == true)
            {                                                                                                                                                                                                                                                                                                                                                                                                            //, Льготник.СНИЛС                     
                queryReestr = "SELECT ЛьготнаяКатегория.ЛьготнаяКатегория AS ['Льготная категория'], Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, Договор.ДатаДоговора, Льготник.СерияДокумента, Льготник.НомерДокумента, Льготник.ДатаВыдачиДокумента, АктВыполнненныхРабот.НомерАкта, Договор.НомерДоговора,АктВыполнненныхРабот.id_акт, АктВыполнненныхРабот.ДатаПодписания,АктВыполнненныхРабот.НомерПоПеречню, Sum(УслугиПоДоговору.Сумма) AS Сумма " +
                              "FROM (ЛьготнаяКатегория INNER JOIN Льготник ON ЛьготнаяКатегория.id_льготнойКатегории = Льготник.id_льготнойКатегории) INNER JOIN ((Договор INNER JOIN АктВыполнненныхРабот ON Договор.id_договор = АктВыполнненныхРабот.id_договор) INNER JOIN УслугиПоДоговору ON Договор.id_договор = УслугиПоДоговору.id_договор) ON Льготник.id_льготник = Договор.id_льготник " +
                              "WHERE (((ЛьготнаяКатегория.ЛьготнаяКатегория)='" + this.cmbЛьготнаяКатегория.Text + "') AND ((Договор.ФлагНаличияАкта)=True)) " +
                              "GROUP BY ЛьготнаяКатегория.ЛьготнаяКатегория, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, Договор.ДатаДоговора, Льготник.СерияДокумента, Льготник.НомерДокумента, Льготник.ДатаВыдачиДокумента, АктВыполнненныхРабот.НомерАкта, Договор.НомерДоговора, АктВыполнненныхРабот.ДатаПодписания,АктВыполнненныхРабот.id_акт,АктВыполнненныхРабот.НомерПоПеречню " + //,Льготник.СНИЛС
                              "HAVING (((АктВыполнненныхРабот.ДатаПодписания)>= " + strДата + " And (АктВыполнненныхРабот.ДатаПодписания)< " + endДата + "))"; //AND (Договор.ФлагНаличияАкта <> True)
            }

            //Выводим реестр заключённых договоров
            if (this.РеестрЗаключенныхДоговоров == false)
            {                                                                                                                                                                                                                                                                       //, Льготник.СНИЛС                            
                queryReestr = "SELECT ЛьготнаяКатегория.ЛьготнаяКатегория AS ['Льготная категория'], Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, Договор.ДатаДоговора, Льготник.СерияДокумента, Льготник.НомерДокумента, Льготник.ДатаВыдачиДокумента, Договор.НомерДоговора, Sum(УслугиПоДоговору.Сумма) AS Сумма " +
                              "FROM (ЛьготнаяКатегория INNER JOIN Льготник ON ЛьготнаяКатегория.id_льготнойКатегории = Льготник.id_льготнойКатегории) INNER JOIN (Договор INNER JOIN УслугиПоДоговору ON Договор.id_договор = УслугиПоДоговору.id_договор) ON Льготник.id_льготник = Договор.id_льготник " +
                              "WHERE (((Договор.ФлагНаличияАкта)=False) and (ЛьготнаяКатегория.ЛьготнаяКатегория)='" + this.cmbЛьготнаяКатегория.Text + "') " +
                              "GROUP BY ЛьготнаяКатегория.ЛьготнаяКатегория, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, Договор.ДатаДоговора, Льготник.СерияДокумента, Льготник.НомерДокумента, Льготник.ДатаВыдачиДокумента, Договор.НомерДоговора " +//, Льготник.СНИЛС
                              "HAVING (([Договор].[ДатаДоговора]>="+  strДата  +" And [Договор].[ДатаДоговора]<" + endДата + "));";
            }

            List<Реестр> list = new List<Реестр>();

            if (this.РеестрЗаключенныхДоговоров == true)
            {

                //List<Реестр> list = new List<Реестр>();
               

                //сформируем данные для рееста
                DataTable dt = ТаблицаБД.GetTable(queryReestr, sCon, "Реестр");

                int iCount = 1;

                decimal sumCount = 0m;

                //заполним list
                foreach (DataRow row in dt.Rows)
                {
                    Реестр str = new Реестр();
                    str.Id_акт = Convert.ToInt32(row["id_акт"]);

                    str.НомерПорядковый = iCount.ToString();

                    string фамилия = row["Фамилия"].ToString();

                    //if (фамилия == "Якямсева")
                    //{
                    //    string asdasd = "test";
                    //}
                    string имя = row["Имя"].ToString();
                    string отчество = row["Отчество"].ToString();

                    str.ФИО = фамилия + " " + имя + " " + отчество;
                    str.НомерДатаДоговора = row["НомерДоговора"].ToString() + " " + Convert.ToDateTime(row["ДатаДоговора"]).ToShortDateString();

                    if (this.РеестрЗаключенныхДоговоров == true)
                    {
                        str.НомерДатаАкта = row["НомерАкта"].ToString() + " " + Convert.ToDateTime(row["ДатаПодписания"]).ToShortDateString();
                    }

                    if (row["НомерПоПеречню"].ToString() == "True")
                    {
                        str.ФлагАктРеестр = true;
                    }
                    else
                    {
                        str.ФлагАктРеестр = false;
                    }

                    str.СерияДатаВыдачиДокумента = row["СерияДокумента"].ToString() + " " + row["НомерДокумента"].ToString() + " " + Convert.ToDateTime(row["ДатаВыдачиДокумента"]).ToShortDateString();
                   // str.SNILS = row["СНИЛС"].ToString();
                    str.СтоимсотьУслуги = row["Сумма"].ToString();

                    sumCount = sumCount + Convert.ToDecimal(str.СтоимсотьУслуги);

                    list.Add(str);
                    iCount++;
                }
            }

            if (this.РеестрЗаключенныхДоговоров == false)
            {
                //queryReestr = "SELECT ЛьготнаяКатегория.ЛьготнаяКатегория AS ['Льготная категория'], Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, Договор.ДатаДоговора, Льготник.СерияДокумента, Льготник.НомерДокумента, Льготник.ДатаВыдачиДокумента, Договор.НомерДоговора, Sum(УслугиПоДоговору.Сумма) AS Сумма " +
                //              "FROM (ЛьготнаяКатегория INNER JOIN Льготник ON ЛьготнаяКатегория.id_льготнойКатегории = Льготник.id_льготнойКатегории) INNER JOIN (Договор INNER JOIN УслугиПоДоговору ON Договор.id_договор = УслугиПоДоговору.id_договор) ON Льготник.id_льготник = Договор.id_льготник " +
                //              "WHERE (((Договор.ФлагНаличияАкта)=False) and (ЛьготнаяКатегория.ЛьготнаяКатегория)='" + this.cmbЛьготнаяКатегория.Text + "')  AND ((Договор.ДатаДоговора) is NULL)" +
                //              "GROUP BY ЛьготнаяКатегория.ЛьготнаяКатегория, Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, Договор.ДатаДоговора, Льготник.СерияДокумента, Льготник.НомерДокумента, Льготник.ДатаВыдачиДокумента, Договор.НомерДоговора ";// +
                //              //"HAVING (([Договор].[ДатаДоговора]>=" + strДата + " And [Договор].[ДатаДоговора]<" + endДата + "));"; // Было закоментировано

                //сформируем данные для рееста
                DataTable dt_null = ТаблицаБД.GetTable(queryReestr, sCon, "Реестр");

                //формируем шапку таблицы
                

                //List<Реестр> list = new List<Реестр>(); // ТЕСТ

                Реестр реестр = new Реестр();
                реестр.НомерПорядковый = "№ п.п";

                реестр.ФИО = "Ф.И.О.";
                реестр.НомерДатаДоговора = "№ и дата договора на предоставление услуг";

                реестр.НомерДатаАкта = "№ и дата акта выполненных работ";
                реестр.СерияДатаВыдачиДокумента = "Серия, № и дата документа о праве на льготу";
               // реестр.SNILS = "СНИЛС";
                реестр.СтоимсотьУслуги = "Стоимость услуги, руб";
                list.Add(реестр);

                //Счётчик
                int iCount = 1;
                decimal sumCount = 0m;

                //заполним list
                foreach (DataRow row in dt_null.Rows)
                {
                    Реестр str = new Реестр();
                    str.НомерПорядковый = iCount.ToString();

                    string фамилия = row["Фамилия"].ToString();

                    //if (фамилия == "Жирнова")
                    //{
                    //   string asdasd = "test";
                    //}

                    string имя = row["Имя"].ToString();
                    string отчество = row["Отчество"].ToString();

                    str.ФИО = фамилия + " " + имя + " " + отчество;

                    if(this.РеестрЗаключенныхДоговоров == false)
                    {
                        str.НомерДатаДоговора = row["НомерДоговора"].ToString() +" " + Convert.ToDateTime(row["ДатаДоговора"]).ToShortDateString();
                    }
                    else
                    {
                        str.НомерДатаДоговора = row["НомерДоговора"].ToString();
                    }

                    if (this.РеестрЗаключенныхДоговоров == true)
                    {
                        str.НомерДатаАкта = row["НомерАкта"].ToString() + " " + Convert.ToDateTime(row["ДатаПодписания"]).ToShortDateString();
                    }

                    str.СерияДатаВыдачиДокумента = row["СерияДокумента"].ToString() + " " + row["НомерДокумента"].ToString() + " " + Convert.ToDateTime(row["ДатаВыдачиДокумента"]).ToShortDateString();
                   // str.SNILS = row["СНИЛС"].ToString();
                    str.СтоимсотьУслуги = row["Сумма"].ToString();

                    sumCount = sumCount + Convert.ToDecimal(str.СтоимсотьУслуги);

                    list.Add(str);
                    iCount++;
                }

                // Подсчитаем строку ИТОГО.
                decimal sumCountPrint = sumCount;

                Реестр strCount = new Реестр();
                strCount.ФИО = "Итого :";
                strCount.СтоимсотьУслуги = Math.Round(sumCount, 2).ToString("c");

                list.Add(strCount);

                //Вставим WORD
                string fNameP = "Реестр " + this.dtStart.Value.ToShortDateString().Replace('.', '_') + " " + this.dtEnd.Value.ToShortDateString().Replace('.', '_');


                try
                {
                    //Скопируем шаблон в папку Документы
                    FileInfo fn = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\Шаблон\Реестр.doc");
                    fn.CopyTo(System.Windows.Forms.Application.StartupPath + @"\Документы\" + fNameP + ".doc", true);
                }
                catch
                {
                    MessageBox.Show("Документ с таки именем уже существует");
                }

                string filName = System.Windows.Forms.Application.StartupPath + @"\Документы\" + fNameP + ".doc";

                //Создаём новый Word.Application
                Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

                //Загружаем документ
                Microsoft.Office.Interop.Word.Document doc = null;

                object fileName = filName;
                object falseValue = false;
                object trueValue = true;
                object missing = Type.Missing;

                doc = app.Documents.Open(ref fileName, ref missing, ref trueValue,
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing);
                
                //зададим левый отступ
                doc.PageSetup.LeftMargin = 40f;


                ////Номер договора
                object wdrepl = WdReplace.wdReplaceAll;
                //object searchtxt = "GreetingLine";
                object searchtxt = "категория";
                object newtxt = (object)this.cmbЛьготнаяКатегория.Text;
                //object frwd = true;
                object frwd = false;
                doc.Content.Find.Execute(ref searchtxt, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd, ref missing, ref missing, ref newtxt, ref wdrepl, ref missing, ref missing,
                ref missing, ref missing);

                //Вставить таблицу
                object bookNaziv = "таблица";
                Range wrdRng = doc.Bookmarks.get_Item(ref  bookNaziv).Range;

                object behavior = Microsoft.Office.Interop.Word.WdDefaultTableBehavior.wdWord8TableBehavior;
                object autobehavior = Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow;


                Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(wrdRng, 1, 6, ref behavior, ref autobehavior);
                table.Range.ParagraphFormat.SpaceAfter = 6;
                table.Columns[1].Width = 40;
                table.Columns[2].Width = 140;
                table.Columns[3].Width = 80;
                table.Columns[4].Width = 110;//ширина столбца с номером акта
                table.Columns[5].Width = 70;
                table.Columns[6].Width = 80;
                table.Borders.Enable = 1; // Рамка - сплошная линия
                table.Range.Font.Name = "Times New Roman";
                //table.Range.Font.Size = 10;
                table.Range.Font.Size = 8;
                //счётчик строк
                int i = 1;

                //запишем данные в таблицу
                foreach (Реестр item in list)
                {
                    table.Cell(i, 1).Range.Text = item.НомерПорядковый;

                    table.Cell(i, 2).Range.Text = item.ФИО;

                    table.Cell(i, 3).Range.Text = item.НомерДатаДоговора;
                    table.Cell(i, 4).Range.Text = item.НомерДатаАкта;
                    table.Cell(i, 5).Range.Text = item.СерияДатаВыдачиДокумента;
                   // table.Cell(i, 6).Range.Text = item.SNILS;
                    table.Cell(i, 6).Range.Text = item.СтоимсотьУслуги;

                    //doc.Words.Count.ToString();
                    Object beforeRow1 = Type.Missing;
                    table.Rows.Add(ref beforeRow1);

                    i++;
                }
                table.Rows[i].Delete();

                //выведим ФИО главврача 
                string глВрач = "select ФИО_ГлавВрач from ГлавВрач where id_главВрач in (select id_главВрач from Поликлинника)";
                DataTable dtГлавВрач = ТаблицаБД.GetTable(глВрач, sCon, "Поликлинника");
                string главВрач = dtГлавВрач.Rows[0][0].ToString();

                //выведим ФИО глав буха
                string глБух = "select ФИО_ГлавБух from ГлавБух where id_главБух in (select id_главБух from Поликлинника)";
                DataTable dtГлавБух = ТаблицаБД.GetTable(глБух, sCon, "Поликлинника");
                string главБух = dtГлавБух.Rows[0][0].ToString();

                ////Номер договора
                object wdrepl2 = WdReplace.wdReplaceAll;
                //object searchtxt = "GreetingLine";
                object searchtxt2 = "главврач";
                object newtxt2 = (object)главВрач;
                //object frwd = true;
                object frwd2 = false;
                doc.Content.Find.Execute(ref searchtxt2, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd2, ref missing, ref missing, ref newtxt2, ref wdrepl2, ref missing, ref missing,
                ref missing, ref missing);


                ////Номер договора
                object wdrepl3 = WdReplace.wdReplaceAll;
                //object searchtxt = "GreetingLine";
                object searchtxt3 = "главбух";
                object newtxt3 = (object)главБух;
                //object frwd = true;
                object frwd3 = false;
                doc.Content.Find.Execute(ref searchtxt3, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd3, ref missing, ref missing, ref newtxt3, ref wdrepl3, ref missing, ref missing,
                ref missing, ref missing);

                //Должность на подписи
                string queryДолжность = "select Должность from ГлавВрач where id_главВрач = (select id_главВрач from Поликлинника)";
                string Predsedatel = ТаблицаБД.GetTable(queryДолжность, ConnectionDB.ConnectionString(), "ФиоШев").Rows[0][0].ToString();

                object wdrepl4 = WdReplace.wdReplaceAll;//39
                //object searchtxt = "GreetingLine";
                object searchtxt4 = "Predsedatel";
                object newtxt4 = (object)Predsedatel;
                //object frwd = true;
                object frwd4 = false;
                doc.Content.Find.Execute(ref searchtxt4, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd4, ref missing, ref missing, ref newtxt4, ref wdrepl4, ref missing, ref missing,
                ref missing, ref missing);

                //должность руководителя ТО
                string quyeryTO = "select Должность, ФИО_Руководитель from ФиоШев where id_шев in (select id_шев from Комитет)";
                DataTable tabРуковод = ТаблицаБД.GetTable(quyeryTO, ConnectionDB.ConnectionString(), "Руководитель");

                //получим должность
                string должность = tabРуковод.Rows[0]["Должность"].ToString();

                object wdrepl5 = WdReplace.wdReplaceAll;//39
                //object searchtxt = "GreetingLine";
                object searchtxt5 = "должность";
                object newtxt5 = (object)должность;
                //object frwd = true;
                object frwd5 = false;
                doc.Content.Find.Execute(ref searchtxt5, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd5, ref missing, ref missing, ref newtxt5, ref wdrepl5, ref missing, ref missing,
                ref missing, ref missing);

                //получим ФИО руководителя ТО
                string руководитель = tabРуковод.Rows[0]["ФИО_Руководитель"].ToString();

                object wdrepl6 = WdReplace.wdReplaceAll;//39
                //object searchtxt = "GreetingLine";
                object searchtxt6 = "руководитель";
                object newtxt6 = (object)руководитель;
                //object frwd = true;
                object frwd6 = false;
                doc.Content.Find.Execute(ref searchtxt6, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd6, ref missing, ref missing, ref newtxt6, ref wdrepl6, ref missing, ref missing,
                ref missing, ref missing);

                app.Visible = true;

                //закроем окно
                this.Close();

                //Выгрузить реестр
                //UnloadDate unloadDate = new UnloadDate(strДата, endДата, this.cmbЛьготнаяКатегория.Text);

                if (this.РеестрЗаключенныхДоговоров == true)
                {



                    UnloadDate unloadDate = new UnloadDate(strДата, endДата, льготнаяКатегорияТекст);
                    List<Unload> unload = unloadDate.Выгрузка();

                    //закроем окно
                    this.Close();

                    //Проверим в файле конфигурации Config.dll разрешена выгрузка реестра в файл или нет
                    using (FileStream fs = File.OpenRead("Config.dll"))
                    using (TextReader read = new StreamReader(fs))
                    {
                        string sConfig = read.ReadLine();
                        if (sConfig == "1")
                        {
                            //Разрешаем выгрузку реестра в файл
                            flagUnLoad = true;
                        }
                        else
                        {
                            //запрещаем выгрузку реестра в файл
                            flagUnLoad = false;
                        }
                    }

                    if (flagUnLoad == true)
                    {

                        //Проверим если список List<Unload> не пустой
                        if (unload.Count != 0)
                        {

                            //получим путь к файлу
                            //SaveFileDialog saveFile = new SaveFileDialg();
                            SaveFileDialog saveFile = new SaveFileDialog();
                            saveFile.DefaultExt = string.Empty;
                            saveFile.Filter = "All files (*.*)|*.*";

                            //Получим красивое название файла
                            //string arDateStart = strДата.Replace('#', '_');
                            ////string arrDateStart = arDateStart.Replace('/', '_');

                            //string arrDateStart = arDateStart.Replace('/', '.');
                            ////string fileNameBg = arrDateStart;

                            //string arEndДата = "_" + this.dtEnd.Value.ToShortDateString();// .Replace('#', '_');
                            ////string arrEndДата = arEndДата.Replace('/', '_');

                            //string arrEndДата = arEndДата.Replace('/', '.');
                            ////string fileNameEnd = arrEndДата;

                            string arrDateStart = Convert.ToDateTime(this.dtStart.Value).ToShortDateString();
                            string arrEndДата = Convert.ToDateTime(this.dtEnd.Value).ToShortDateString();

                            saveFile.FileName = arrDateStart + "_" + льготнаяКатегорияТекст + "_" + arrEndДата + ".r";
                            //saveFile.ShowDialog();

                            string fileBinaryName = string.Empty;


                            if (saveFile.ShowDialog() == DialogResult.OK)
                            {
                                fileBinaryName = saveFile.FileName;
                                //saveFile.InitialDirectory = @".\";
                                //WorkingDirectory
                            }
                            else
                            {
                                return;
                            }

                            //сериализуем список 
                            FileStream fs = new FileStream(fileBinaryName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                            BinaryFormatter bf = new BinaryFormatter();

                            //сериализация
                            bf.Serialize(fs, unload);

                            //Освободим в потоке все ресурсы
                            fs.Dispose();
                            fs.Close();

                            //Установим для проги текущую директорию для корректного считывания пути к БД
                            Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath;
                        }
                    }
                }

                //Конец
            }


            if (this.РеестрЗаключенныхДоговоров == true)
            {

                //сформируем word
                string fName = "Реестр " + this.dtStart.Value.ToShortDateString().Replace('.', '_') + " " + this.dtEnd.Value.ToShortDateString().Replace('.', '_');

                //Выведим данные в отдельное окошко
                FormListReestr listR = new FormListReestr();

                //передадим в форму данные полученные от SQL Server 
                listR.РеестрАктов = list;

                //Передадим начало и конец отчётного периода
                listR.dtStart = this.dtStart.Value;
                listR.dtEnd = this.dtEnd.Value;

                //передадим льготную категорию
                listR.ЛьготнаяКатегория = this.cmbЛьготнаяКатегория.Text.Trim();

                //передадим флаг о разрешении выгрузки реестров заключённых договоров
                listR.РеестрЗаключенныхДоговоров = this.РеестрЗаключенныхДоговоров;

                //Отобразим форму выбора актов выполненных работ
                listR.Show();

                //Закроем форму
                this.Close();
            }

            //распечатаем word
            //FileInfo fnDel = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\Документы\Акт4.dot");
            // FileInfo fnDel = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\Документы\"+fileName+".doc");
            //fnDel.Delete();
            //try
            //{
            //    //Скопируем шаблон в папку Документы
            //    FileInfo fn = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\Шаблон\Реестр.doc");
            //    fn.CopyTo(System.Windows.Forms.Application.StartupPath + @"\Документы\" + fName + ".doc", true);
            //}
            //catch
            //{
            //    MessageBox.Show("Документ с таки именем уже существует");
            //}

            //string filName = System.Windows.Forms.Application.StartupPath + @"\Документы\" + fName + ".doc";

            ////Создаём новый Word.Application
            //Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

            ////Загружаем документ
            //Microsoft.Office.Interop.Word.Document doc = null;

            //object fileName = filName;
            //object falseValue = false;
            //object trueValue = true;
            //object missing = Type.Missing;

            //doc = app.Documents.Open(ref fileName, ref missing, ref trueValue,
            //ref missing, ref missing, ref missing, ref missing, ref missing,
            //ref missing, ref missing, ref missing, ref missing, ref missing,
            //ref missing, ref missing, ref missing);

            ////зададим левый отступ
            //doc.PageSetup.LeftMargin = 40f;


            //////Номер договора
            //object wdrepl = WdReplace.wdReplaceAll;
            ////object searchtxt = "GreetingLine";
            //object searchtxt = "категория";
            //object newtxt = (object)this.cmbЛьготнаяКатегория.Text;
            ////object frwd = true;
            //object frwd = false;
            //doc.Content.Find.Execute(ref searchtxt, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd, ref missing, ref missing, ref newtxt, ref wdrepl, ref missing, ref missing,
            //ref missing, ref missing);

            ////Вставить таблицу
            //object bookNaziv = "таблица";
            //Range wrdRng = doc.Bookmarks.get_Item(ref  bookNaziv).Range;

            //object behavior = Microsoft.Office.Interop.Word.WdDefaultTableBehavior.wdWord8TableBehavior;
            //object autobehavior = Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow;


            //Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(wrdRng, 1, 6, ref behavior, ref autobehavior);
            //table.Range.ParagraphFormat.SpaceAfter = 6;
            //table.Columns[1].Width = 40;
            //table.Columns[2].Width = 140;
            //table.Columns[3].Width = 80;
            //table.Columns[4].Width = 110;//ширина столбца с номером акта
            //table.Columns[5].Width = 70;
            //table.Columns[6].Width = 80;
            //table.Borders.Enable = 1; // Рамка - сплошная линия
            //table.Range.Font.Name = "Times New Roman";
            ////table.Range.Font.Size = 10;
            //table.Range.Font.Size = 8;
            ////счётчик строк
            //int i = 1;

            ////запишем данные в таблицу
            //foreach (Реестр item in list)
            //{
            //    table.Cell(i, 1).Range.Text = item.НомерПорядковый;

            //    table.Cell(i, 2).Range.Text = item.ФИО;

            //    table.Cell(i, 3).Range.Text = item.НомерДатаДоговора;
            //    table.Cell(i, 4).Range.Text = item.НомерДатаАкта;
            //    table.Cell(i, 5).Range.Text = item.СерияДатаВыдачиДокумента;
            //    table.Cell(i, 6).Range.Text = item.СтоимсотьУслуги;

            //    //doc.Words.Count.ToString();
            //    Object beforeRow1 = Type.Missing;
            //    table.Rows.Add(ref beforeRow1);

            //    i++;
            //}
            //table.Rows[i].Delete();

            ////выведим ФИО главврача 
            //string глВрач = "select ФИО_ГлавВрач from ГлавВрач where id_главВрач in (select id_главВрач from Поликлинника)";
            //DataTable dtГлавВрач = ТаблицаБД.GetTable(глВрач, sCon, "Поликлинника");
            //string главВрач = dtГлавВрач.Rows[0][0].ToString();

            ////выведим ФИО глав буха
            //string глБух = "select ФИО_ГлавБух from ГлавБух where id_главБух in (select id_главБух from Поликлинника)";
            //DataTable dtГлавБух = ТаблицаБД.GetTable(глБух, sCon, "Поликлинника");
            //string главБух = dtГлавБух.Rows[0][0].ToString();

            //////Номер договора
            //object wdrepl2 = WdReplace.wdReplaceAll;
            ////object searchtxt = "GreetingLine";
            //object searchtxt2 = "главврач";
            //object newtxt2 = (object)главВрач;
            ////object frwd = true;
            //object frwd2 = false;
            //doc.Content.Find.Execute(ref searchtxt2, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd2, ref missing, ref missing, ref newtxt2, ref wdrepl2, ref missing, ref missing,
            //ref missing, ref missing);


            //////Номер договора
            //object wdrepl3 = WdReplace.wdReplaceAll;
            ////object searchtxt = "GreetingLine";
            //object searchtxt3 = "главбух";
            //object newtxt3 = (object)главБух;
            ////object frwd = true;
            //object frwd3 = false;
            //doc.Content.Find.Execute(ref searchtxt3, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd3, ref missing, ref missing, ref newtxt3, ref wdrepl3, ref missing, ref missing,
            //ref missing, ref missing);

            ////Должность на подписи
            //string queryДолжность = "select Должность from ГлавВрач where id_главВрач = (select id_главВрач from Поликлинника)";
            //string Predsedatel = ТаблицаБД.GetTable(queryДолжность, ConnectionDB.ConnectionString(), "ФиоШев").Rows[0][0].ToString();

            //object wdrepl4 = WdReplace.wdReplaceAll;//39
            ////object searchtxt = "GreetingLine";
            //object searchtxt4 = "Predsedatel";
            //object newtxt4 = (object)Predsedatel;
            ////object frwd = true;
            //object frwd4 = false;
            //doc.Content.Find.Execute(ref searchtxt4, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd4, ref missing, ref missing, ref newtxt4, ref wdrepl4, ref missing, ref missing,
            //ref missing, ref missing);

            ////должность руководителя ТО
            //string quyeryTO = "select Должность, ФИО_Руководитель from ФиоШев where id_шев in (select id_шев from Комитет)";
            //DataTable tabРуковод = ТаблицаБД.GetTable(quyeryTO, ConnectionDB.ConnectionString(), "Руководитель");

            ////получим должность
            //string должность = tabРуковод.Rows[0]["Должность"].ToString();

            //object wdrepl5 = WdReplace.wdReplaceAll;//39
            ////object searchtxt = "GreetingLine";
            //object searchtxt5 = "должность";
            //object newtxt5 = (object)должность;
            ////object frwd = true;
            //object frwd5 = false;
            //doc.Content.Find.Execute(ref searchtxt5, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd5, ref missing, ref missing, ref newtxt5, ref wdrepl5, ref missing, ref missing,
            //ref missing, ref missing);

            ////получим ФИО руководителя ТО
            //string руководитель = tabРуковод.Rows[0]["ФИО_Руководитель"].ToString();

            //object wdrepl6 = WdReplace.wdReplaceAll;//39
            ////object searchtxt = "GreetingLine";
            //object searchtxt6 = "руководитель";
            //object newtxt6 = (object)руководитель;
            ////object frwd = true;
            //object frwd6 = false;
            //doc.Content.Find.Execute(ref searchtxt6, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd6, ref missing, ref missing, ref newtxt6, ref wdrepl6, ref missing, ref missing,
            //ref missing, ref missing);

            //app.Visible = true;

            ////закроем окно
            //this.Close();

            ////Выгрузить реестр
            ////UnloadDate unloadDate = new UnloadDate(strДата, endДата, this.cmbЛьготнаяКатегория.Text);

            //if(this.РеестрЗаключенныхДоговоров == true)
            //{
            //UnloadDate unloadDate = new UnloadDate(strДата, endДата, льготнаяКатегорияТекст);
            //List<Unload> unload = unloadDate.Выгрузка();

            ////закроем окно
            //this.Close();

            ////Проверим в файле конфигурации Config.dll разрешена выгрузка реестра в файл или нет
            //using (FileStream fs = File.OpenRead("Config.dll"))
            //using (TextReader read = new StreamReader(fs))
            //{
            //    string sConfig = read.ReadLine();
            //    if (sConfig == "1")
            //    {
            //        //Разрешаем выгрузку реестра в файл
            //        flagUnLoad = true;
            //    }
            //    else
            //    {
            //        //запрещаем выгрузку реестра в файл
            //        flagUnLoad = false;
            //    }
            //}

            //if (flagUnLoad == true)
            //{

            //    //Проверим если список List<Unload> не пустой
            //    if (unload.Count != 0)
            //    {

            //        //получим путь к файлу
            //        //SaveFileDialog saveFile = new SaveFileDialg();
            //        SaveFileDialog saveFile = new SaveFileDialog();
            //        saveFile.DefaultExt = string.Empty;
            //        saveFile.Filter = "All files (*.*)|*.*";

            //        //Получим красивое название файла
            //        //string arDateStart = strДата.Replace('#', '_');
            //        ////string arrDateStart = arDateStart.Replace('/', '_');
                    
            //        //string arrDateStart = arDateStart.Replace('/', '.');
            //        ////string fileNameBg = arrDateStart;

            //        //string arEndДата = "_" + this.dtEnd.Value.ToShortDateString();// .Replace('#', '_');
            //        ////string arrEndДата = arEndДата.Replace('/', '_');
                   
            //        //string arrEndДата = arEndДата.Replace('/', '.');
            //        ////string fileNameEnd = arrEndДата;

            //        string arrDateStart = Convert.ToDateTime(this.dtStart.Value).ToShortDateString();
            //        string arrEndДата = Convert.ToDateTime(this.dtEnd.Value).ToShortDateString();

            //        saveFile.FileName =  arrDateStart + "_" + льготнаяКатегорияТекст + "_" + arrEndДата + ".r";
            //        //saveFile.ShowDialog();

            //        string fileBinaryName = string.Empty;


            //        if (saveFile.ShowDialog() == DialogResult.OK)
            //        {
            //            fileBinaryName = saveFile.FileName;
            //            //saveFile.InitialDirectory = @".\";
            //            //WorkingDirectory
            //        }
            //        else
            //        {
            //            return;
            //        }

            //        //сериализуем список 
            //        FileStream fs = new FileStream(fileBinaryName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            //        BinaryFormatter bf = new BinaryFormatter();

            //        //сериализация
            //        bf.Serialize(fs, unload);

            //        //Освободим в потоке все ресурсы
            //        fs.Dispose();
            //        fs.Close();

            //        //Установим для проги текущую директорию для корректного считывания пути к БД
            //        Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath;
            //    }
            //}


            //}



        }

        private void FormReestrPrint_Load(object sender, EventArgs e)
        {
            string sCon = ConnectionDB.ConnectionString();
            string queryЛК = "select * from ЛьготнаяКатегория";
            DataTable dtЛК = ТаблицаБД.GetTable(queryЛК, sCon, "ЛьготнаяКатегория");

            this.cmbЛьготнаяКатегория.DataSource = dtЛК;
            this.cmbЛьготнаяКатегория.DisplayMember = "ЛьготнаяКатегория";

        }
    }
}