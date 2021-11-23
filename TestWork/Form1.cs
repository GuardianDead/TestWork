using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TestWorkLibrary.Models;
using TestWorkLibrary.Parser;
using TestWorkLibrary.Validators;

namespace TestWork
{
    public partial class Form1 : Form
    {
        private List<Character> _dataCharacters = new List<Character>();
        private Character _selectedCharacter { get => _dataCharacters.Single(character => character.Id == comboBox_Selected_Character.SelectedItem.ToString()); }
        private Supply _selectedSupply { get => _selectedCharacter.Supplies.Items.Single(item => item.Name == listBox_Supplies.SelectedItem.ToString()); }

        private readonly SuppliesCharactersXMLParser _characterXMLParser = new SuppliesCharactersXMLParser();
        private string _currentFilePath;

        public Form1()
        {
            InitializeComponent();
        }

        #region Загрузка и сохранения данных
        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _currentFilePath = openFileDialog1.FileName;
            CleanDataCharactersAndControllers();
            _dataCharacters.AddRange(_characterXMLParser.ReadFile(_currentFilePath));
            ReFillCharacters(_dataCharacters);
            if (_dataCharacters.Count != 0)
            {
                comboBox_Selected_Character.SelectedIndex = 0;
            }
        }
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_currentFilePath is null || _currentFilePath == "")
            {
                MessageBox.Show("Файл еще не был загружен", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _characterXMLParser.SaveInSameFile(_currentFilePath, _dataCharacters.ToArray());
            MessageBox.Show("Данные успешно сохранены", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            _currentFilePath = saveFileDialog1.FileName;
            _characterXMLParser.SaveFile(_currentFilePath, _dataCharacters.ToArray());
            MessageBox.Show("Данные успешно сохранены", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region События выбранных данных в listBox-ах и comboBox
        private void comboBox_Selected_Character_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReFillSupplies(_selectedCharacter.Supplies.Items.Select(item => item.Name));
            if (listBox_Supplies.Items.Count != 0)
                listBox_Supplies.SelectedIndex = 0;
            ReFillIncludes(_selectedCharacter.Supplies.Includes);
            ReFillPreview(_selectedCharacter.Supplies);
        }
        private void listBox_Supplies_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReFillAddons(_selectedSupply.Addons);
            textBox_Supply_Name.Text = _selectedSupply.Name;
            textBox_Supply_Count.Text = _selectedSupply.Count.ToString();
            textBox_Supply_Condition.Text = _selectedSupply.Condition.ToString();
            textBox_Supply_Probability.Text = _selectedSupply.Probability.ToString();
        }
        private void listBox_Addons_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox_Addon_Name.Text = listBox_Addons.SelectedItem.ToString() ?? "";
        }
        private void listBox_Includes_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox_Include.Text = listBox_Includes.SelectedItem.ToString() ?? "";
        }
        #endregion

        #region Методы заполнения и очистки listBox-ов, textBox-ов, comboBox
        private void ReFillCharacters(IEnumerable<Character> characters)
        {
            comboBox_Selected_Character.Items.Clear();
            comboBox_Selected_Character.Items.AddRange(_dataCharacters.Select(character => (object)character.Id).ToArray());
        }
        private void ReFillSupplies(IEnumerable<string> suppliesName)
        {
            listBox_Supplies.Items.Clear();
            listBox_Supplies.Items.AddRange(suppliesName.ToArray());
        }
        private void ReFillIncludes(IEnumerable<string> includes)
        {
            listBox_Includes.Items.Clear();
            listBox_Includes.Items.AddRange(includes.ToArray());
        }
        private void ReFillAddons(IEnumerable<string> addons)
        {
            listBox_Addons.Items.Clear();
            listBox_Addons.Items.AddRange(addons.ToArray());
        }
        private void ReFillPreview(Supplies supplies)
        {
            listBox_Preview.Items.Clear();
            listBox_Preview.Items.Add(@$"[{supplies.Spawn}] \n");
            foreach (var supplyItem in supplies.Items)
            {
                if (supplyItem.Addons.Count() != 0)
                    listBox_Preview.Items.Add(@$"{string.Join(", ", $"{supplyItem.Name} = {supplyItem.Count}", string.Join(", ", supplyItem.Addons), @$"cond={supplyItem.Condition}", @$"prob={supplyItem.Probability}")} \n");
                else
                    listBox_Preview.Items.Add(@$"{string.Join(", ", $"{supplyItem.Name} = {supplyItem.Count}", $"cond={supplyItem.Condition}", $"prob={supplyItem.Probability}")} \n");
            }
            foreach (var include in supplies.Includes)
            {
                listBox_Preview.Items.Add(@$"#include '{include}' \n".Replace("'", '"'.ToString()));
            }
        }
        private void CleanDataCharactersAndControllers()
        {
            _dataCharacters.Clear();
            foreach (var control in Controls)
            {
                if (control is ListBox) ((ListBox)control).Items.Clear();
                if (control is TextBox) ((TextBox)control).Text = "";
            }
        }
        #endregion

        #region Добавление/изменение/удаление и работа с данными Supply
        private void button_Create_Supply_Click(object sender, EventArgs e)
        {
            if (comboBox_Selected_Character.SelectedIndex == -1)
            {
                MessageBox.Show("Персонаж для добавления предмета не выбран", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!CheckInputsForSupply())
            {
                return;
            }

            var newSupply = CreateSupplyFromInputs();
            var resultValudate = new SupplyValidator().Validate(newSupply);
            if (_selectedCharacter.Supplies.Items.Any(supply => supply.Name == newSupply.Name))
            {
                MessageBox.Show("Данное имя предмета уже содержится  в снаряжении", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!resultValudate.IsValid)
            {
                MessageBox.Show($"Данные не соответствуют требованиями, подробнее об ошибке {string.Join(Environment.NewLine, resultValudate.Errors)}", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _selectedCharacter.Supplies.Items.Add(newSupply);
            ReFillSupplies(_selectedCharacter.Supplies.Items.Select(supply => supply.Name));
            ReFillPreview(_selectedCharacter.Supplies);
        }
        private void button_Change_Suplly_Click(object sender, EventArgs e)
        {
            if (comboBox_Selected_Character.SelectedIndex == -1)
            {
                MessageBox.Show("Персонаж для изменения предмета не выбран", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (listBox_Supplies.SelectedIndex == -1)
            {
                MessageBox.Show("Предмет снаряжения не выбран", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!CheckInputsForSupply())
            {
                return;
            }

            var newSupply = CreateSupplyFromInputs();
            var resultValudate = new SupplyValidator().Validate(newSupply);
            if (!resultValudate.IsValid)
            {
                MessageBox.Show($"Данные не соответствуют требованиями, подробнее об ошибке {string.Join(Environment.NewLine, resultValudate.Errors)}", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var oldSupplyIndex = _selectedCharacter.Supplies.Items.FindIndex(item => item.Name == listBox_Supplies.SelectedItem.ToString());
            if (oldSupplyIndex == -1)
            {
                MessageBox.Show("Данного предмета не существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _selectedCharacter.Supplies.Items[oldSupplyIndex] = newSupply;
            ReFillSupplies(_selectedCharacter.Supplies.Items.Select(item => item.Name));
            ReFillPreview(_selectedCharacter.Supplies);
        }
        private void button_Delete_Supply_Click(object sender, EventArgs e)
        {
            if (comboBox_Selected_Character.SelectedIndex == -1)
            {
                MessageBox.Show("Персонаж для удаления предмета не выбран", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var deletedSupplyName = textBox_Supply_Name.Text;
            if (deletedSupplyName == "")
            {
                MessageBox.Show("Поле имени предмета незаполнено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var recivedSupply = _selectedCharacter.Supplies.Items.SingleOrDefault(item => item.Name == deletedSupplyName);
            if (recivedSupply is null)
            {
                MessageBox.Show("Данного предмета не существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var recivedRemoveSupply = _selectedCharacter.Supplies.Items.SingleOrDefault(item => item.Name == deletedSupplyName);
            _selectedCharacter.Supplies.Items.Remove(recivedRemoveSupply);
            ReFillSupplies(_selectedCharacter.Supplies.Items.Select(item => item.Name));
            ReFillPreview(_selectedCharacter.Supplies);
        }

        private Supply CreateSupplyFromInputs()
        {
            var addons = new List<string>();
            foreach (var addon in listBox_Addons.Items)
                addons.Add(addon.ToString());
            var supply = new Supply(
                    textBox_Supply_Name.Text,
                    addons,
                    Convert.ToInt32(textBox_Supply_Count.Text),
                    Convert.ToDouble(textBox_Supply_Condition.Text.Replace('.', ',')),
                    Convert.ToDouble(textBox_Supply_Probability.Text.Replace('.', ','))
                );
            return supply;
        }
        private bool CheckInputsForSupply()
        {
            if (textBox_Supply_Name.Text == "" || textBox_Supply_Count.Text == ""
                || textBox_Supply_Condition.Text == "" || textBox_Supply_Probability.Text == "")
            {
                MessageBox.Show("Не все данные для предмета заполнены", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                Convert.ToInt32(textBox_Supply_Count.Text);
                Convert.ToDouble(textBox_Supply_Condition.Text.Replace('.', ','));
                Convert.ToDouble(textBox_Supply_Probability.Text.Replace('.', ','));
            }
            catch
            {
                MessageBox.Show("Данные неверного формата", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            return true;
        }
        #endregion

        #region Добавление/изменение/удаление и работа с данными Addons
        private void button_Add_Addon_Click(object sender, EventArgs e)
        {
            if (listBox_Supplies.SelectedIndex == -1)
            {
                MessageBox.Show("Предмет для добавления аддона не выбран", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var addonName = textBox_Addon_Name.Text;
            if (addonName == "")
            {
                MessageBox.Show("Поле для имени аддона не заполнено", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_selectedSupply.Addons.Any(addon => addon == addonName))
            {
                MessageBox.Show("Данный аддон уже есть у предмета", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _selectedSupply.Addons.Add(addonName);
            ReFillAddons(_selectedSupply.Addons);
            ReFillPreview(_selectedCharacter.Supplies);
        }
        private void button_Change_Addon_Click(object sender, EventArgs e)
        {
            if (listBox_Supplies.SelectedIndex == -1)
            {
                MessageBox.Show("Предмет для добавления аддона не выбран", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var addonName = textBox_Addon_Name.Text;
            if (listBox_Addons.SelectedIndex == -1)
            {
                MessageBox.Show("Аддон для изменения не выбран", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (addonName == "")
            {
                MessageBox.Show("Поле для имени аддона не заполнено", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedAddonIndex = _selectedSupply.Addons.FindIndex(addon => addon == listBox_Addons.SelectedItem.ToString());
            if (selectedAddonIndex == -1)
            {
                MessageBox.Show("", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _selectedSupply.Addons[selectedAddonIndex] = addonName;
            ReFillAddons(_selectedSupply.Addons);
            ReFillPreview(_selectedCharacter.Supplies);
        }
        private void button_Delete_Addon_Click(object sender, EventArgs e)
        {
            if (listBox_Supplies.SelectedIndex == -1)
            {
                MessageBox.Show("Предмет для удаления аддона не выбран", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var addonName = textBox_Addon_Name.Text;
            if (addonName == "")
            {
                MessageBox.Show("Поле для имени аддона не заполнено", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_selectedSupply.Addons.Any(addon => addon == addonName))
            {
                MessageBox.Show("Данного аддона нет у предмета", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _selectedSupply.Addons.Remove(addonName);
            ReFillAddons(_selectedSupply.Addons);
            ReFillPreview(_selectedCharacter.Supplies);
        }
        #endregion

        #region Добавление/изменение/удаление и работа с данными Includes
        private void button_Add_Include_Click(object sender, EventArgs e)
        {
            if (comboBox_Selected_Character.SelectedIndex == -1)
            {
                MessageBox.Show("Персонаж для добавления модуля не выбран", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var includePath = textBox_Include.Text;
            if (includePath == "")
            {
                MessageBox.Show("Поле для пути модуля не заполнено", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_selectedCharacter.Supplies.Includes.Any(include => include == includePath))
            {
                MessageBox.Show("Данный модуль уже существует", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _selectedCharacter.Supplies.Includes.Add(includePath);
            ReFillIncludes(_selectedCharacter.Supplies.Includes);
            ReFillPreview(_selectedCharacter.Supplies);
        }
        private void button_Change_Include_Click(object sender, EventArgs e)
        {
            if (comboBox_Selected_Character.SelectedIndex == -1)
            {
                MessageBox.Show("Персонаж для изменения модуля не выбран", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var includePath = textBox_Include.Text;
            if (listBox_Includes.SelectedIndex == -1)
            {
                MessageBox.Show("Изменяемый модуль не выбран", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (includePath == "")
            {
                MessageBox.Show("Поле для пути модуля не заполнено", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedIncludeIndex = _selectedCharacter.Supplies.Includes.FindIndex(include => include == listBox_Includes.SelectedItem.ToString());
            if (selectedIncludeIndex == -1)
            {
                MessageBox.Show("Данного модуля не существует", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _selectedCharacter.Supplies.Includes[selectedIncludeIndex] = includePath;
            ReFillIncludes(_selectedCharacter.Supplies.Includes);
            ReFillPreview(_selectedCharacter.Supplies);
        }
        private void button_Delete_Include_Click(object sender, EventArgs e)
        {
            if (comboBox_Selected_Character.SelectedIndex == -1)
            {
                MessageBox.Show("Персонаж для удаления модуля не выбран", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var includePath = textBox_Include.Text;
            if (includePath == "")
            {
                MessageBox.Show("Поле для пути модуля не заполнено", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!_selectedCharacter.Supplies.Includes.Any(include => include == includePath))
            {
                MessageBox.Show("Данного модуля не существует", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _selectedCharacter.Supplies.Includes.Remove(includePath);
            ReFillIncludes(_selectedCharacter.Supplies.Includes);
            ReFillPreview(_selectedCharacter.Supplies);
        }
        #endregion
    }
}

