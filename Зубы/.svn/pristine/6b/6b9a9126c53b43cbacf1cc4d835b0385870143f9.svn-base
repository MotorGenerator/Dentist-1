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

namespace Стамотология
{
    public partial class FormPrintContract : Form
    {
        public FormPrintContract()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string queryPrint = "SELECT Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, " +
                              "Договор.НомерДоговора, Договор.ДатаДоговора, " +
                              "АктВыполнненныхРабот.НомерАкта, АктВыполнненныхРабот.ДатаПодписания, " +
                              "Врач.ФИО, Договор.НомерТехЛиста, Договор.id_договор, ВрачОплата.СчётФактура, " +
                              "ВрачОплата.id, Sum(УслугиПоДоговору.Сумма) AS [Sum-Сумма], ВрачОплата.Дата, " +
                              "Льготник.СерияДокумента, Льготник.НомерДокумента, Льготник.ДатаВыдачиДокумента, ЛьготнаяКатегория.ЛьготнаяКатегория " +
                              "FROM ((ВрачОплата INNER JOIN (((Льготник INNER JOIN Договор ON Льготник.id_льготник = Договор.id_льготник) " +
                              "INNER JOIN АктВыполнненныхРабот ON Договор.id_договор = АктВыполнненныхРабот.id_договор) " +
                              "INNER JOIN Врач ON Договор.id_врач = Врач.id_врач) ON ВрачОплата.idДоговор = Договор.id_договор) " +
                              "INNER JOIN УслугиПоДоговору ON Договор.id_договор = УслугиПоДоговору.id_договор) " +
                              "INNER JOIN ЛьготнаяКатегория ON Льготник.id_льготнойКатегории = ЛьготнаяКатегория.id_льготнойКатегории " +
                              "GROUP BY Льготник.Фамилия, Льготник.Имя, Льготник.Отчество, Договор.НомерДоговора, Договор.ДатаДоговора, " +
                              "АктВыполнненныхРабот.НомерАкта, АктВыполнненныхРабот.ДатаПодписания, Врач.ФИО, Договор.НомерТехЛиста, " +
                              "Договор.id_договор, ВрачОплата.СчётФактура, ВрачОплата.id, ВрачОплата.Дата, Льготник.СерияДокумента, Льготник.НомерДокумента, " +
                              "Льготник.ДатаВыдачиДокумента, ЛьготнаяКатегория.ЛьготнаяКатегория " +
                              "HAVING (((ВрачОплата.СчётФактура)='" + this.txtNum.Text.Trim() + "' ));";


            DataTable tabPrint = ТаблицаБД.GetTable(queryPrint, ConnectionDB.ConnectionString(), "РеестрПечать");
            
            // Проверка на наличие данных
            if (tabPrint.Rows.Count == 0)
                return;

            //сформируем word
            //string fName = "Реестр " + this.dtStart.Value.ToShortDateString().Replace('.', '_') + " " + this.dtEnd.Value.ToShortDateString().Replace('.', '_');
            //string fName = "РеестрТехЛист";
            string fName = "РеестрСчётФактура";

            try
            {

                if (File.Exists(System.Windows.Forms.Application.StartupPath + @"\Документы\" + fName + ".doc") == true)
                {
                    //Удаим все файлы из папки.
                    File.Delete(System.Windows.Forms.Application.StartupPath + @"\Документы\" + fName + ".doc");
                }

                //Скопируем шаблон в папку Документы
                FileInfo fn = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\Шаблон\РеестрСчётФактура.doc");
                fn.CopyTo(System.Windows.Forms.Application.StartupPath + @"\Документы\" + fName + ".doc", true);
            }
            catch
            {
                MessageBox.Show("Документ с таки именем уже существует, либо произошла ошибка");
                return;
            }

            string filName = System.Windows.Forms.Application.StartupPath + @"\Документы\" + fName + ".doc";


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

            doc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;//ориентация альбомная


            ////Номер договора
            object wdrepl = WdReplace.wdReplaceAll;
            //object searchtxt = "GreetingLine";
            object searchtxt = "категория";           
            object newtxt = (object)tabPrint.Rows[0]["ЛьготнаяКатегория"].ToString().Trim();// +" - счёт-фактура " + this.txtNum.Text.Trim(); 
            //object frwd = true;
            object frwd = false;
            doc.Content.Find.Execute(ref searchtxt, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd, ref missing, ref missing, ref newtxt, ref wdrepl, ref missing, ref missing,
            ref missing, ref missing);

            //Вставить таблицу
            object bookNaziv = "таблица";
            Range wrdRng = doc.Bookmarks.get_Item(ref  bookNaziv).Range;

            object behavior = Microsoft.Office.Interop.Word.WdDefaultTableBehavior.wdWord8TableBehavior;
            object autobehavior = Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow;


            Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(wrdRng, 1, 8, ref behavior, ref autobehavior);
            table.Range.ParagraphFormat.SpaceAfter = 6;
            table.Columns[1].Width = 40;
            table.Columns[2].Width = 170;
            table.Columns[3].Width = 80;
            table.Columns[4].Width = 110;//ширина столбца с номером акта
            table.Columns[5].Width = 70;
            table.Columns[6].Width = 70;
            table.Columns[7].Width = 100;
            table.Columns[8].Width = 70;
            table.Borders.Enable = 1; // Рамка - сплошная линия
            table.Range.Font.Name = "Times New Roman";
            //table.Range.Font.Size = 10;
            table.Range.Font.Size = 8;

            //счётчик строк
            int k = 1;

            decimal summCount = 0.0m;

            // Список классов для хранения отчёта.
            List<ОтчётТехЛист> listPrint = new List<ОтчётТехЛист>();

            ОтчётТехЛист headItem = new ОтчётТехЛист();

            // Запишем шапку таблицы.
            // Выведим порядковый номер.

            headItem.НомерПП = "№ п.п.";

            // Выведим ФИО 
            headItem.ФиоЛьготника = "Ф.И.О.";

            // Номер и дата договора. 
            headItem.НомерДатаДоговора = "№ и дата договора на предоставление услуг";

            // № и дата акта выполненных работ. 
            headItem.НомерДатаАкта = "№ и дата акта выполненных работ";

            // Серия, номер и дата документа о праве на льготу. Льготник.СерияДокумента, Льготник.НомерДокумента, Льготник.ДатаВыдачиДокумента
            headItem.СерияНомерДок = "Серия, № и дата документа о праве на льготу";

            // Стоимост услуг.
            headItem.СтоимостьУслуг = "Стоимость услуи, руб.";

            // ФИО врача
            headItem.ФиоВрач = "Ф.И.О. врача-протезиста";

            // Номер тех листа.
            headItem.НомерТехЛиста = "Номер тех. листа";

            listPrint.Add(headItem);

            // Заполним список данными.
            foreach (DataRow item in tabPrint.Rows)
            {
                ОтчётТехЛист it = new ОтчётТехЛист();

                // Выведим порядковый номер.
                it.НомерПП = k.ToString();

                // Выведим ФИО 
                it.ФиоЛьготника = item["Фамилия"].ToString().Trim() + " " + item["Имя"].ToString().Trim() + " " + item["Отчество"].ToString().Trim();

                // Номер и дата договора. 
                it.НомерДатаДоговора = item["НомерДоговора"].ToString().Trim() + " " + Convert.ToDateTime(item["ДатаДоговора"]).ToShortDateString();

                // № и дата акта выполненных работ. 
                it.НомерДатаАкта = item["НомерАкта"].ToString().Trim() + " " + Convert.ToDateTime(item["ДатаПодписания"]).ToShortDateString();

                // Серия, номер и дата документа о праве на льготу. Льготник.СерияДокумента, Льготник.НомерДокумента, Льготник.ДатаВыдачиДокумента
                it.СерияНомерДок = item["СерияДокумента"].ToString().Trim() + " " + item["НомерДокумента"].ToString().Trim() + " " + Convert.ToDateTime(item["ДатаВыдачиДокумента"]).ToShortDateString();

                decimal summ = Convert.ToDecimal(item["Sum-Сумма"]);

                // Подсчитываем сумму.
                summCount = Math.Round(summCount + summ, 2);

                // Стоимост услуг.
                it.СтоимостьУслуг = summ.ToString("c").Trim();

                // ФИО врача
                it.ФиоВрач = item["ФИО"].ToString().Trim();

                // Номер тех листа.
                it.НомерТехЛиста = item["НомерТехЛиста"].ToString().Trim();

                listPrint.Add(it);

                k++;

            }

            // Поместим в подвал отчёта сумму.
            ОтчётТехЛист butt = new ОтчётТехЛист();
            butt.ФиоЛьготника = "Итого: ";
            butt.СтоимостьУслуг = summCount.ToString("c");

            listPrint.Add(butt);

            //счётчик строк
            int i = 1;

            //Выведим данные на печать.
            foreach (ОтчётТехЛист item2 in listPrint)
            {
                // Выведим порядковый номер.
                table.Cell(i, 1).Range.Text = item2.НомерПП;

                // Выведим ФИО 
                table.Cell(i, 2).Range.Text = item2.ФиоЛьготника;

                // Номер и дата договора. 
                table.Cell(i, 3).Range.Text = item2.НомерДатаДоговора;

                // № и дата акта выполненных работ. 
                table.Cell(i, 4).Range.Text = item2.НомерДатаАкта;

                // Серия, номер и дата документа о праве на льготу. Льготник.СерияДокумента, Льготник.НомерДокумента, Льготник.ДатаВыдачиДокумента
                table.Cell(i, 5).Range.Text = item2.СерияНомерДок;

                // Стоимост услуг.
                table.Cell(i, 6).Range.Text = item2.СтоимостьУслуг;

                // ФИО врача
                table.Cell(i, 7).Range.Text = item2.ФиоВрач;

                // Номер тех листа.
                table.Cell(i, 8).Range.Text = item2.НомерТехЛиста;

                //doc.Words.Count.ToString();
                Object beforeRow = Type.Missing;
                table.Rows.Add(ref beforeRow);

                i++;

            }

            table.Rows[i].Delete();

            ////Номер договора
            object wdrepl2 = WdReplace.wdReplaceAll;
            //object searchtxt = "GreetingLine";
            object searchtxt2 = "numAcc";
            object newtxt2 = (object)this.txtNum.Text.Trim();
            //object frwd = true;
            object frwd2 = false;
            doc.Content.Find.Execute(ref searchtxt2, ref missing, ref missing, ref missing, ref missing, ref missing, ref frwd2, ref missing, ref missing, ref newtxt2, ref wdrepl2, ref missing, ref missing,
            ref missing, ref missing);

            app.Visible = true;

            //закроем окно
            this.Close();

        }
    }
}