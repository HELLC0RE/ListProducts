namespace ListProducts
{
    partial class Products_List
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanelProducts = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelPagination = new System.Windows.Forms.FlowLayoutPanel();
            this.searchText = new System.Windows.Forms.TextBox();
            this.sortBox = new System.Windows.Forms.ComboBox();
            this.filterBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // flowLayoutPanelProducts
            // 
            this.flowLayoutPanelProducts.Location = new System.Drawing.Point(22, 59);
            this.flowLayoutPanelProducts.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanelProducts.Name = "flowLayoutPanelProducts";
            this.flowLayoutPanelProducts.Size = new System.Drawing.Size(737, 500);
            this.flowLayoutPanelProducts.TabIndex = 0;
            // 
            // flowLayoutPanelPagination
            // 
            this.flowLayoutPanelPagination.Location = new System.Drawing.Point(569, 570);
            this.flowLayoutPanelPagination.Margin = new System.Windows.Forms.Padding(680, 590, 3, 3);
            this.flowLayoutPanelPagination.Name = "flowLayoutPanelPagination";
            this.flowLayoutPanelPagination.Size = new System.Drawing.Size(190, 30);
            this.flowLayoutPanelPagination.TabIndex = 0;
            // 
            // searchText
            // 
            this.searchText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.searchText.Location = new System.Drawing.Point(22, 15);
            this.searchText.Margin = new System.Windows.Forms.Padding(2);
            this.searchText.MaxLength = 200;
            this.searchText.Name = "searchText";
            this.searchText.Size = new System.Drawing.Size(350, 26);
            this.searchText.TabIndex = 1;
            this.searchText.Enter += new System.EventHandler(this.SearchText_Enter);
            this.searchText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SearchText_KeyPress);
            this.searchText.Leave += new System.EventHandler(this.SearchText_Leave);
            // 
            // sortBox
            // 
            this.sortBox.DropDownWidth = 189;
            this.sortBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sortBox.FormattingEnabled = true;
            this.sortBox.Items.AddRange(new object[] {
            "По умолчанию",
            "По наименованию (по возрастанию)",
            "По наименованию (по убыванию)",
            "По номеру производственного цеха (по возрастанию)",
            "По номеру производственного цеха (по убыванию)",
            "По стоимости (по возрастанию)",
            "По стоимости (по убыванию)"});
            this.sortBox.Location = new System.Drawing.Point(376, 15);
            this.sortBox.Margin = new System.Windows.Forms.Padding(2);
            this.sortBox.Name = "sortBox";
            this.sortBox.Size = new System.Drawing.Size(190, 23);
            this.sortBox.TabIndex = 2;
            this.sortBox.Text = "Сортировка";
            this.sortBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.SortBox_DrawItem);
            this.sortBox.SelectedIndexChanged += new System.EventHandler(this.SortBox_SelectedIndexChanged);
            // 
            // filterBox
            // 
            this.filterBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.filterBox.FormattingEnabled = true;
            this.filterBox.Location = new System.Drawing.Point(569, 15);
            this.filterBox.Margin = new System.Windows.Forms.Padding(2);
            this.filterBox.Name = "filterBox";
            this.filterBox.Size = new System.Drawing.Size(190, 23);
            this.filterBox.TabIndex = 3;
            this.filterBox.Text = "Фильтрация";
            this.filterBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.filterBox_DrawItem);
            this.filterBox.SelectedIndexChanged += new System.EventHandler(this.FilterBox_SelectedIndexChanged);
            // 
            // Products_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.flowLayoutPanelPagination);
            this.Controls.Add(this.filterBox);
            this.Controls.Add(this.sortBox);
            this.Controls.Add(this.searchText);
            this.Controls.Add(this.flowLayoutPanelProducts);
            this.MaximumSize = new System.Drawing.Size(800, 700);
            this.MinimumSize = new System.Drawing.Size(800, 700);
            this.Name = "Products_List";
            this.Text = "Список продуктов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelProducts;
        private System.Windows.Forms.TextBox searchText;
        private System.Windows.Forms.ComboBox sortBox;
        private System.Windows.Forms.ComboBox filterBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPagination;
    }
}

