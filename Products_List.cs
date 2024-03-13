using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using ListProducts;
using ListProducts.Properties;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;

namespace ListProducts
{
    public partial class Products_List : Form
    {
        private DbMethods dbMethods;
        private List<Product> productsList;
        private List<string> typesList;
        private const int itemsPerPage = 5;
        private int currentPage = 1;
        private int offset;
        private int limit;
        private int totalPages;

        public Products_List()
        {
            InitializeComponent();
            dbMethods = new DbMethods();
            productsList = new List<Product>();
            searchText.Text = "Введите для поиска";
            productsList = DbMethods.GetAllProducts();
            typesList = DbMethods.GetProductTypes();
            DisplayPage(currentPage, null, null, null);
            sortBox.DrawMode = DrawMode.OwnerDrawFixed;
            sortBox.ItemHeight = 30;
            filterBox.DrawMode = DrawMode.OwnerDrawFixed;
            filterBox.ItemHeight = 30;
            filterBox.Items.Add("Все типы");
            filterBox.Items.AddRange(typesList.ToArray());
        }

        private void DisplayPage(int page, string text, string sorter, string filter)
        {
            flowLayoutPanelProducts.Controls.Clear();
            flowLayoutPanelPagination.Controls.Clear();

            offset = (page - 1) * itemsPerPage;
            limit = itemsPerPage;


            var filteredProducts = productsList;

            if (!string.IsNullOrEmpty(text) && text != "Введите для поиска")
            {
                filteredProducts = filteredProducts
                    .Where(p => p.Title.Contains(text) ||
                                p.Description.Contains(text))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(sorter) && sorter != "Сортировка")
            {
                switch (sorter)
                {
                    case "По наименованию (по возрастанию)":
                        filteredProducts = filteredProducts.OrderBy(p => p.Title).ToList();
                        break;
                    case "По наименованию (по убыванию)":
                        filteredProducts = filteredProducts.OrderByDescending(p => p.Title).ToList();
                        break;
                    case "По номеру производственного цеха (по возрастанию)":
                        filteredProducts = filteredProducts.OrderBy(p => p.ProductShopNumber).ToList();
                        break;
                    case "По номеру производственного цеха (по убыванию)":
                        filteredProducts = filteredProducts.OrderByDescending(p => p.ProductShopNumber).ToList();
                        break;
                    case "По стоимости (по возрастанию)":
                        filteredProducts = filteredProducts.OrderBy(p => p.Cost).ToList();
                        break;
                    case "По стоимости (по убыванию)":
                        filteredProducts = filteredProducts.OrderByDescending(p => p.Cost).ToList();
                        break;
                    default:
                        break;
                }
            }

            if (!string.IsNullOrEmpty(filter) && filter != "Фильтрация" && filter != "Все типы")
            {
                    int typeID = 0;
                    switch (filter)
                    {
                        case "Электроника":
                            typeID = 1;
                            break;
                        case "Одежда":
                            typeID = 2;
                            break;
                        case "Еда":
                            typeID = 3;
                            break;
                        case "Книги":
                            typeID = 4;
                            break;
                        default:
                            break;
                    }
                    if (typeID != 0)
                    {
                        filteredProducts = filteredProducts.Where(p => p.Type == typeID).ToList();
                    }               
            }

            int resultCount = filteredProducts.Count;
            filteredProducts = filteredProducts.Skip(offset).Take(limit).ToList();

            for (int i = 0; i < filteredProducts.Count; i++)
            {
                DisplayProduct(filteredProducts[i]);
            }

            DisplayPagination(resultCount);
        }
      
        private void DisplayProduct(Product product)
        {
            Panel productPanel = new Panel
            {
                Height = 90,
                Width = flowLayoutPanelProducts.Width - 5,
                BorderStyle = BorderStyle.FixedSingle,
            };

            PictureBox pictureBox = new PictureBox
            {
                Image = Properties.Resources.product_icon,
                SizeMode = PictureBoxSizeMode.CenterImage,
                Location = new Point(20, 5),
                Width = 85,
                Height = 80,
            };
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            Label attributesLabel = new Label
            {
                Text = $"Артикул: \n{product.Article}",
                Location = new Point(130, 20),
                Width = 150,
                Height = 50,
                Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 10)
            };

            attributesLabel.Click += (sender, e) =>
            {
                string fullText = ((Label)sender).Text;
                string articleNum = fullText.Split(' ')[1].Trim();
                DrawBarcode(articleNum);
            };

            Label nameLabel = new Label
            {
                Text = product.Title,
                Location = new Point(300, 20),
                Width = 200,
                Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold)
            };

            Label descriptionLabel = new Label
            {
                Text = product.Description,
                Location = new Point(300, 45),
                Width = 200,
                Height = 100,
            };

            Label costLabel = new Label
            {
                Text = $"Стоимость: \n {product.Cost:C}",
                Location = new Point(flowLayoutPanelProducts.Width - 150, 20),
                Width = 100,
                Height = 50,
                Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold)
            };

            productPanel.Controls.Add(pictureBox);
            productPanel.Controls.Add(attributesLabel);
            productPanel.Controls.Add(nameLabel);
            productPanel.Controls.Add(descriptionLabel);
            productPanel.Controls.Add(costLabel);

            flowLayoutPanelProducts.Controls.Add(productPanel);
        }

        private void DisplayPagination(int count)
        {
            totalPages = (int)Math.Ceiling((double)count / itemsPerPage);

            flowLayoutPanelPagination.RightToLeft = RightToLeft.Yes;

            AddPageButton("<", currentPage + 1);

            for (int i = totalPages; i != 0; i--)
            {
                Label pageButton = new Label
                {
                    Text = i.ToString(),
                    Width = 20,
                    Height = 20,
                    Margin = new Padding(5),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Cursor = Cursors.Hand
                };

                if (i == currentPage)
                {
                    pageButton.Font = new System.Drawing.Font(pageButton.Font, FontStyle.Bold | FontStyle.Underline);
                }

                pageButton.Click += (sender, e) =>
                {
                    currentPage = int.Parse(((Label)sender).Text);
                    DisplayPage(currentPage, searchText.Text, sortBox.Text, filterBox.Text);
                };

                flowLayoutPanelPagination.Controls.Add(pageButton);
            }

            AddPageButton(">", currentPage - 1);
        }

        private void AddPageButton(string buttonText, int targetPage)
        {
            Label pageButton = new Label
            {
                Text = buttonText,
                Width = 15,
                Height = 15,
                Margin = new Padding(5),
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand
            };

            pageButton.Click += (sender, e) =>
            {
                if (targetPage > 0 && targetPage <= totalPages)
                {
                    currentPage = targetPage;
                    DisplayPage(currentPage, searchText.Text, sortBox.Text, filterBox.Text);
                }
            };

            flowLayoutPanelPagination.Controls.Add(pageButton);
        }

        private void SearchText_Enter(object sender, EventArgs e)
        {
            if (searchText.Text.Equals("Введите для поиска"))
            {
                searchText.Clear();
                searchText.ForeColor = Color.Black;
            }
        }

        private void SearchText_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchText.Text))
            {
                searchText.Text = "Введите для поиска";
                searchText.ForeColor = Color.Gray;
            }
        }

        private void SearchText_KeyPress(object sender, KeyPressEventArgs e)
        {
            currentPage = 1;
            DisplayPage(currentPage, searchText.Text, sortBox.Text, filterBox.Text);
        }

        private void SortBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            using (SolidBrush brush = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(sortBox.Items[e.Index].ToString(), e.Font, brush, e.Bounds);
            }
            e.DrawFocusRectangle();
        }

        private void filterBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            using (SolidBrush brush = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(filterBox.Items[e.Index].ToString(), e.Font, brush, e.Bounds);
            }
            e.DrawFocusRectangle();
        }

        private void SortBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPage = 1;
            DisplayPage(currentPage, searchText.Text, sortBox.Text, filterBox.Text);
        }

        private void FilterBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPage = 1;
            DisplayPage(currentPage, searchText.Text, sortBox.Text, filterBox.Text);
        }
 
        public void DrawBarcode(string code, int resolution = 20) // resolution - пикселей на миллиметр
        {
            int numberCount = 13; // количество цифр
            float height = 25.93f * resolution; // высота штрих кода
            float lineHeight = 22.85f * resolution; // высота штриха
            float leftOffset = 3.63f * resolution; // свободная зона слева
            float rightOffset = 2.31f * resolution; // свободная зона справа
                                                    //штрихи, которые образуют правый и левый ограничивающие знаки,
                                                    //а также центральный ограничивающий знак должны быть удлинены вниз на 1,65мм
            float longLineHeight = lineHeight + 1.65f * resolution;
            float fontHeight = 2.75f * resolution; // высота цифр
            float lineToFontOffset = 0.165f * resolution; // минимальный размер от верхнего края цифр до нижнего края штрихов
            float lineWidthDelta = 0.15f * resolution; // ширина 0.15*{цифра}
            float lineWidthFull = 1.35f * resolution; // ширина белой полоски при 0 или 0.15*9
            float lineOffset = 0.2f * resolution; // между штрихами должно быть расстояние в 0.2мм

            float width = leftOffset + rightOffset + 6 * (lineWidthDelta + lineOffset) + numberCount * (lineWidthFull + lineOffset); // ширина штрих-кода

            Bitmap bitmap = new Bitmap((int)width, (int)height); // создание картинки нужных размеров
            Graphics g = Graphics.FromImage(bitmap); // создание графики

            System.Drawing.Font font = new System.Drawing.Font("Arial", fontHeight, FontStyle.Regular, GraphicsUnit.Pixel); // создание шрифта

            StringFormat fontFormat = new StringFormat(); // Центрирование текста
            fontFormat.Alignment = StringAlignment.Center;
            fontFormat.LineAlignment = StringAlignment.Center;

            float x = leftOffset; // позиция рисования по x
            for (int i = 0; i < numberCount; i++)
            {
                int number = Convert.ToInt32(code[i].ToString()); // число из кода
                if (number != 0)
                {
                    g.FillRectangle(Brushes.Black, x, 0, number * lineWidthDelta, lineHeight); // рисуем штрих
                }
                RectangleF fontRect = new RectangleF(x, lineHeight + lineToFontOffset, lineWidthFull, fontHeight); // рамки для буквы
                g.DrawString(code[i].ToString(), font, Brushes.Black, fontRect, fontFormat); // рисуем букву
                x += lineWidthFull + lineOffset; // смещаем позицию рисования по x


                if (i == 0 || i == numberCount / 2 || i == numberCount - 1) // если это начало, середина или конец кода рисуем разделители
                {
                    for (int j = 0; j < 2; j++) // рисуем 2 линии разделителя
                    {
                        g.FillRectangle(Brushes.Black, x, 0, lineWidthDelta, longLineHeight); // рисуем длинный штрих
                        x += lineWidthDelta + lineOffset; // смещаем позицию рисования по x
                    }
                }
            }
            Hide();
            BarcodeForm barcodeForm = new BarcodeForm();
            barcodeForm.barcode = bitmap;
            barcodeForm.Code = code;
            barcodeForm.FormClosed += (s, args) => Close();
            barcodeForm.ShowDialog();
        }
    }
}       
       
