
namespace TestWork
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.listBox_Supplies = new System.Windows.Forms.ListBox();
            this.label_Supplais = new System.Windows.Forms.Label();
            this.comboBox_Selected_Character = new System.Windows.Forms.ComboBox();
            this.label_Person = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Supply_Name = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_Supply_Count = new System.Windows.Forms.TextBox();
            this.textBox_Supply_Probability = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_Supply_Condition = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button_Create_Supply = new System.Windows.Forms.Button();
            this.button_Change_Suplly = new System.Windows.Forms.Button();
            this.listBox_Includes = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_Include = new System.Windows.Forms.TextBox();
            this.button_Add_Include = new System.Windows.Forms.Button();
            this.button_Delete_Supply = new System.Windows.Forms.Button();
            this.button_Change_Include = new System.Windows.Forms.Button();
            this.button_Delete_Include = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox_Addons = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label_Addon = new System.Windows.Forms.Label();
            this.textBox_Addon_Name = new System.Windows.Forms.TextBox();
            this.button_Delete_Addon = new System.Windows.Forms.Button();
            this.button_Change_Addon = new System.Windows.Forms.Button();
            this.button_Add_Addon = new System.Windows.Forms.Button();
            this.richTextBox_Preview = new System.Windows.Forms.RichTextBox();
            this.checkBox_Supply_Probability = new System.Windows.Forms.CheckBox();
            this.checkBox_Supply_Condition = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "XML файл|*.xml|Все файлы|*.*";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "XML файл|*.xml|Все файлы|*.*";
            // 
            // listBox_Supplies
            // 
            this.listBox_Supplies.FormattingEnabled = true;
            this.listBox_Supplies.ItemHeight = 20;
            this.listBox_Supplies.Location = new System.Drawing.Point(12, 113);
            this.listBox_Supplies.Name = "listBox_Supplies";
            this.listBox_Supplies.Size = new System.Drawing.Size(246, 264);
            this.listBox_Supplies.TabIndex = 2;
            this.listBox_Supplies.SelectedIndexChanged += new System.EventHandler(this.listBox_Supplies_SelectedIndexChanged);
            // 
            // label_Supplais
            // 
            this.label_Supplais.AutoSize = true;
            this.label_Supplais.Location = new System.Drawing.Point(10, 90);
            this.label_Supplais.Name = "label_Supplais";
            this.label_Supplais.Size = new System.Drawing.Size(179, 20);
            this.label_Supplais.TabIndex = 3;
            this.label_Supplais.Text = "Снаряжение персонажа";
            // 
            // comboBox_Selected_Character
            // 
            this.comboBox_Selected_Character.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Selected_Character.FormattingEnabled = true;
            this.comboBox_Selected_Character.Location = new System.Drawing.Point(10, 59);
            this.comboBox_Selected_Character.Name = "comboBox_Selected_Character";
            this.comboBox_Selected_Character.Size = new System.Drawing.Size(249, 28);
            this.comboBox_Selected_Character.TabIndex = 4;
            this.comboBox_Selected_Character.SelectedIndexChanged += new System.EventHandler(this.comboBox_Selected_Character_SelectedIndexChanged);
            // 
            // label_Person
            // 
            this.label_Person.AutoSize = true;
            this.label_Person.Location = new System.Drawing.Point(10, 36);
            this.label_Person.Name = "label_Person";
            this.label_Person.Size = new System.Drawing.Size(186, 20);
            this.label_Person.TabIndex = 5;
            this.label_Person.Text = "Индификатор персонажа";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(478, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Предпросмотр данных";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(264, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Название предмета";
            // 
            // textBox_Supply_Name
            // 
            this.textBox_Supply_Name.Location = new System.Drawing.Point(265, 60);
            this.textBox_Supply_Name.Name = "textBox_Supply_Name";
            this.textBox_Supply_Name.Size = new System.Drawing.Size(207, 27);
            this.textBox_Supply_Name.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(264, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Количество (>0)";
            // 
            // textBox_Supply_Count
            // 
            this.textBox_Supply_Count.Location = new System.Drawing.Point(264, 113);
            this.textBox_Supply_Count.Name = "textBox_Supply_Count";
            this.textBox_Supply_Count.Size = new System.Drawing.Size(208, 27);
            this.textBox_Supply_Count.TabIndex = 11;
            // 
            // textBox_Supply_Probability
            // 
            this.textBox_Supply_Probability.Location = new System.Drawing.Point(265, 166);
            this.textBox_Supply_Probability.Name = "textBox_Supply_Probability";
            this.textBox_Supply_Probability.Size = new System.Drawing.Size(38, 27);
            this.textBox_Supply_Probability.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(264, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(203, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "Шанс выпададения (0.0-1.0)";
            // 
            // textBox_Supply_Condition
            // 
            this.textBox_Supply_Condition.Location = new System.Drawing.Point(263, 223);
            this.textBox_Supply_Condition.Name = "textBox_Supply_Condition";
            this.textBox_Supply_Condition.Size = new System.Drawing.Size(40, 27);
            this.textBox_Supply_Condition.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(263, 200);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(141, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "Состояние (0.0-1.0)";
            // 
            // button_Create_Supply
            // 
            this.button_Create_Supply.Location = new System.Drawing.Point(263, 258);
            this.button_Create_Supply.Name = "button_Create_Supply";
            this.button_Create_Supply.Size = new System.Drawing.Size(207, 37);
            this.button_Create_Supply.TabIndex = 17;
            this.button_Create_Supply.Text = "Создать";
            this.button_Create_Supply.UseVisualStyleBackColor = true;
            this.button_Create_Supply.Click += new System.EventHandler(this.button_Create_Supply_Click);
            // 
            // button_Change_Suplly
            // 
            this.button_Change_Suplly.Location = new System.Drawing.Point(263, 301);
            this.button_Change_Suplly.Name = "button_Change_Suplly";
            this.button_Change_Suplly.Size = new System.Drawing.Size(207, 37);
            this.button_Change_Suplly.TabIndex = 18;
            this.button_Change_Suplly.Text = "Изменить";
            this.button_Change_Suplly.UseVisualStyleBackColor = true;
            this.button_Change_Suplly.Click += new System.EventHandler(this.button_Change_Suplly_Click);
            // 
            // listBox_Includes
            // 
            this.listBox_Includes.FormattingEnabled = true;
            this.listBox_Includes.ItemHeight = 20;
            this.listBox_Includes.Location = new System.Drawing.Point(10, 604);
            this.listBox_Includes.Name = "listBox_Includes";
            this.listBox_Includes.Size = new System.Drawing.Size(456, 164);
            this.listBox_Includes.TabIndex = 20;
            this.listBox_Includes.SelectedIndexChanged += new System.EventHandler(this.listBox_Includes_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 581);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(174, 20);
            this.label6.TabIndex = 21;
            this.label6.Text = "Подключаемые модули";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 771);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(117, 20);
            this.label7.TabIndex = 22;
            this.label7.Text = "Путь до модуля";
            // 
            // textBox_Include
            // 
            this.textBox_Include.Location = new System.Drawing.Point(10, 794);
            this.textBox_Include.Name = "textBox_Include";
            this.textBox_Include.Size = new System.Drawing.Size(456, 27);
            this.textBox_Include.TabIndex = 23;
            // 
            // button_Add_Include
            // 
            this.button_Add_Include.Location = new System.Drawing.Point(10, 827);
            this.button_Add_Include.Name = "button_Add_Include";
            this.button_Add_Include.Size = new System.Drawing.Size(145, 37);
            this.button_Add_Include.TabIndex = 24;
            this.button_Add_Include.Text = "Создать";
            this.button_Add_Include.UseVisualStyleBackColor = true;
            this.button_Add_Include.Click += new System.EventHandler(this.button_Add_Include_Click);
            // 
            // button_Delete_Supply
            // 
            this.button_Delete_Supply.Location = new System.Drawing.Point(263, 344);
            this.button_Delete_Supply.Name = "button_Delete_Supply";
            this.button_Delete_Supply.Size = new System.Drawing.Size(207, 37);
            this.button_Delete_Supply.TabIndex = 25;
            this.button_Delete_Supply.Text = "Удалить";
            this.button_Delete_Supply.UseVisualStyleBackColor = true;
            this.button_Delete_Supply.Click += new System.EventHandler(this.button_Delete_Supply_Click);
            // 
            // button_Change_Include
            // 
            this.button_Change_Include.Location = new System.Drawing.Point(161, 826);
            this.button_Change_Include.Name = "button_Change_Include";
            this.button_Change_Include.Size = new System.Drawing.Size(153, 37);
            this.button_Change_Include.TabIndex = 26;
            this.button_Change_Include.Text = "Изменить";
            this.button_Change_Include.UseVisualStyleBackColor = true;
            this.button_Change_Include.Click += new System.EventHandler(this.button_Change_Include_Click);
            // 
            // button_Delete_Include
            // 
            this.button_Delete_Include.Location = new System.Drawing.Point(320, 826);
            this.button_Delete_Include.Name = "button_Delete_Include";
            this.button_Delete_Include.Size = new System.Drawing.Size(146, 37);
            this.button_Delete_Include.TabIndex = 27;
            this.button_Delete_Include.Text = "Удалить";
            this.button_Delete_Include.UseVisualStyleBackColor = true;
            this.button_Delete_Include.Click += new System.EventHandler(this.button_Delete_Include_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(976, 28);
            this.menuStrip1.TabIndex = 29;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.загрузитьToolStripMenuItem,
            this.сохранитьToolStripMenuItem,
            this.сохранитьКакToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // загрузитьToolStripMenuItem
            // 
            this.загрузитьToolStripMenuItem.Name = "загрузитьToolStripMenuItem";
            this.загрузитьToolStripMenuItem.Size = new System.Drawing.Size(192, 26);
            this.загрузитьToolStripMenuItem.Text = "Загрузить ";
            this.загрузитьToolStripMenuItem.Click += new System.EventHandler(this.загрузитьToolStripMenuItem_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(192, 26);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // сохранитьКакToolStripMenuItem
            // 
            this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
            this.сохранитьКакToolStripMenuItem.Size = new System.Drawing.Size(192, 26);
            this.сохранитьКакToolStripMenuItem.Text = "Сохранить как";
            this.сохранитьКакToolStripMenuItem.Click += new System.EventHandler(this.сохранитьКакToolStripMenuItem_Click);
            // 
            // listBox_Addons
            // 
            this.listBox_Addons.FormattingEnabled = true;
            this.listBox_Addons.ItemHeight = 20;
            this.listBox_Addons.Location = new System.Drawing.Point(10, 410);
            this.listBox_Addons.Name = "listBox_Addons";
            this.listBox_Addons.Size = new System.Drawing.Size(243, 164);
            this.listBox_Addons.TabIndex = 30;
            this.listBox_Addons.SelectedIndexChanged += new System.EventHandler(this.listBox_Addons_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 387);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(135, 20);
            this.label8.TabIndex = 31;
            this.label8.Text = "Аддоны предмета";
            // 
            // label_Addon
            // 
            this.label_Addon.AutoSize = true;
            this.label_Addon.Location = new System.Drawing.Point(259, 395);
            this.label_Addon.Name = "label_Addon";
            this.label_Addon.Size = new System.Drawing.Size(131, 20);
            this.label_Addon.TabIndex = 32;
            this.label_Addon.Text = "Название аддона";
            // 
            // textBox_Addon_Name
            // 
            this.textBox_Addon_Name.Location = new System.Drawing.Point(262, 418);
            this.textBox_Addon_Name.Name = "textBox_Addon_Name";
            this.textBox_Addon_Name.Size = new System.Drawing.Size(206, 27);
            this.textBox_Addon_Name.TabIndex = 33;
            // 
            // button_Delete_Addon
            // 
            this.button_Delete_Addon.Location = new System.Drawing.Point(261, 537);
            this.button_Delete_Addon.Name = "button_Delete_Addon";
            this.button_Delete_Addon.Size = new System.Drawing.Size(207, 37);
            this.button_Delete_Addon.TabIndex = 36;
            this.button_Delete_Addon.Text = "Удалить";
            this.button_Delete_Addon.UseVisualStyleBackColor = true;
            this.button_Delete_Addon.Click += new System.EventHandler(this.button_Delete_Addon_Click);
            // 
            // button_Change_Addon
            // 
            this.button_Change_Addon.Location = new System.Drawing.Point(261, 494);
            this.button_Change_Addon.Name = "button_Change_Addon";
            this.button_Change_Addon.Size = new System.Drawing.Size(207, 37);
            this.button_Change_Addon.TabIndex = 35;
            this.button_Change_Addon.Text = "Изменить";
            this.button_Change_Addon.UseVisualStyleBackColor = true;
            this.button_Change_Addon.Click += new System.EventHandler(this.button_Change_Addon_Click);
            // 
            // button_Add_Addon
            // 
            this.button_Add_Addon.Location = new System.Drawing.Point(261, 451);
            this.button_Add_Addon.Name = "button_Add_Addon";
            this.button_Add_Addon.Size = new System.Drawing.Size(207, 37);
            this.button_Add_Addon.TabIndex = 34;
            this.button_Add_Addon.Text = "Добавить";
            this.button_Add_Addon.UseVisualStyleBackColor = true;
            this.button_Add_Addon.Click += new System.EventHandler(this.button_Add_Addon_Click);
            // 
            // richTextBox_Preview
            // 
            this.richTextBox_Preview.Location = new System.Drawing.Point(478, 59);
            this.richTextBox_Preview.Name = "richTextBox_Preview";
            this.richTextBox_Preview.Size = new System.Drawing.Size(486, 804);
            this.richTextBox_Preview.TabIndex = 37;
            this.richTextBox_Preview.Text = "";
            // 
            // checkBox_Supply_Probability
            // 
            this.checkBox_Supply_Probability.AutoSize = true;
            this.checkBox_Supply_Probability.Location = new System.Drawing.Point(309, 168);
            this.checkBox_Supply_Probability.Name = "checkBox_Supply_Probability";
            this.checkBox_Supply_Probability.Size = new System.Drawing.Size(163, 24);
            this.checkBox_Supply_Probability.TabIndex = 38;
            this.checkBox_Supply_Probability.Text = "Учитывать в файле";
            this.checkBox_Supply_Probability.UseVisualStyleBackColor = true;
            this.checkBox_Supply_Probability.CheckedChanged += new System.EventHandler(this.checkBox_Supply_Probability_CheckedChanged);
            // 
            // checkBox_Supply_Condition
            // 
            this.checkBox_Supply_Condition.AutoSize = true;
            this.checkBox_Supply_Condition.Location = new System.Drawing.Point(309, 226);
            this.checkBox_Supply_Condition.Name = "checkBox_Supply_Condition";
            this.checkBox_Supply_Condition.Size = new System.Drawing.Size(163, 24);
            this.checkBox_Supply_Condition.TabIndex = 39;
            this.checkBox_Supply_Condition.Text = "Учитывать в файле";
            this.checkBox_Supply_Condition.UseVisualStyleBackColor = true;
            this.checkBox_Supply_Condition.CheckedChanged += new System.EventHandler(this.checkBox_Supply_Condition_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 873);
            this.Controls.Add(this.checkBox_Supply_Condition);
            this.Controls.Add(this.checkBox_Supply_Probability);
            this.Controls.Add(this.richTextBox_Preview);
            this.Controls.Add(this.button_Delete_Addon);
            this.Controls.Add(this.button_Change_Addon);
            this.Controls.Add(this.button_Add_Addon);
            this.Controls.Add(this.textBox_Addon_Name);
            this.Controls.Add(this.label_Addon);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.listBox_Addons);
            this.Controls.Add(this.button_Delete_Include);
            this.Controls.Add(this.button_Change_Include);
            this.Controls.Add(this.button_Delete_Supply);
            this.Controls.Add(this.button_Add_Include);
            this.Controls.Add(this.textBox_Include);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.listBox_Includes);
            this.Controls.Add(this.button_Change_Suplly);
            this.Controls.Add(this.button_Create_Supply);
            this.Controls.Add(this.textBox_Supply_Condition);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_Supply_Probability);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_Supply_Count);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_Supply_Name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_Person);
            this.Controls.Add(this.comboBox_Selected_Character);
            this.Controls.Add(this.label_Supplais);
            this.Controls.Add(this.listBox_Supplies);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактор снаряжения";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ListBox listBox_Supplies;
        private System.Windows.Forms.Label label_Supplais;
        private System.Windows.Forms.ComboBox comboBox_Selected_Character;
        private System.Windows.Forms.Label label_Person;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Supply_Name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_Supply_Count;
        private System.Windows.Forms.TextBox textBox_Supply_Probability;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_Supply_Condition;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_Create_Supply;
        private System.Windows.Forms.Button button_Change_Suplly;
        private System.Windows.Forms.ListBox listBox_Includes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_Include;
        private System.Windows.Forms.Button button_Add_Include;
        private System.Windows.Forms.Button button_Delete_Supply;
        private System.Windows.Forms.Button button_Change_Include;
        private System.Windows.Forms.Button button_Delete_Include;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьКакToolStripMenuItem;
        private System.Windows.Forms.ListBox listBox_Addons;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label_Addon;
        private System.Windows.Forms.TextBox textBox_Addon_Name;
        private System.Windows.Forms.Button button_Delete_Addon;
        private System.Windows.Forms.Button button_Change_Addon;
        private System.Windows.Forms.Button button_Add_Addon;
        private System.Windows.Forms.RichTextBox richTextBox_Preview;
        private System.Windows.Forms.CheckBox checkBox_Supply_Probability;
        private System.Windows.Forms.CheckBox checkBox_Supply_Condition;
    }
}

