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

        private string _currentFilePath;

        public Form1()
        {
            InitializeComponent();
        }

        #region Загрузка и сохранения данных
        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            _currentFilePath = openFileDialog1.FileName;
            CleanDataCharactersAndControllers();
            _dataCharacters.AddRange(SuppliesCharactersParser.ReadFile(_currentFilePath));
            ReFillCharacters(_dataCharacters);
            if (_dataCharacters.Count != 0)
                comboBox_Selected_Character.SelectedIndex = 0;
        }
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_currentFilePath is null || _currentFilePath == "")
            {
                MessageBox.Show("Файл еще не был загружен", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SuppliesCharactersParser.SaveInSameFile(_currentFilePath, _dataCharacters.ToArray());
            MessageBox.Show("Данные успешно сохранены", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            _currentFilePath = saveFileDialog1.FileName;
            SuppliesCharactersParser.SaveFile(_currentFilePath, _dataCharacters.ToArray());
            MessageBox.Show("Данные успешно сохранены", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Различные события элементов
        private void comboBox_Selected_Character_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReFillSupplies(_selectedCharacter.Supplies.Items.Select(item => item.Name));
            if (listBox_Supplies.Items.Count != 0)
                listBox_Supplies.SelectedIndex = 0;
            ReFillIncludes(_selectedCharacter.Supplies.Includes);
            ReFillPreview(_selectedCharacter);
        }
        private void listBox_Supplies_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReFillAddons(_selectedSupply.Addons);
            textBox_Supply_Name.Text = _selectedSupply.Name;
            textBox_Supply_Count.Text = _selectedSupply.Count.ToString();
            textBox_Supply_Condition.Text = _selectedSupply.Condition.ToString().Replace(',', '.');
            textBox_Supply_Probability.Text = _selectedSupply.Probability.ToString().Replace(',', '.');
            checkBox_Supply_Condition.Checked = _selectedSupply.HasFillCondition;
            checkBox_Supply_Probability.Checked = _selectedSupply.HasFillProbability;
        }
        private void listBox_Addons_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox_Addon_Name.Text = listBox_Addons.SelectedItem?.ToString() ?? "";
        }
        private void listBox_Includes_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox_Include.Text = listBox_Includes.SelectedItem?.ToString() ?? "";
        }
        private void checkBox_Supply_Probability_CheckedChanged(object sender, EventArgs e)
        {
            _selectedSupply.HasFillProbability = checkBox_Supply_Probability.Checked;
            ReFillPreview(_selectedCharacter);
        }
        private void checkBox_Supply_Condition_CheckedChanged(object sender, EventArgs e)
        {
            _selectedSupply.HasFillCondition = checkBox_Supply_Condition.Checked;
            ReFillPreview(_selectedCharacter);
        }
        #endregion

        #region Методы заполнения и очистки элементов
        private void ReFillCharacters(IEnumerable<Character> characters)
        {
            comboBox_Selected_Character.Items.Clear();
            comboBox_Selected_Character.Items.AddRange(characters.Select(character => (object)character.Id).ToArray());
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
        private void ReFillPreview(Character character)
        {
            richTextBox_Preview.Text = "[spawn] \\n" + Environment.NewLine;
            foreach (var item in character.Supplies.Items)
            {
                richTextBox_Preview.Text += $"{item.Name} = {item.Count}";
                if (item.Addons.Count != 0) richTextBox_Preview.Text += $", {string.Join(", ", item.Addons)}";
                if (item.HasFillProbability) richTextBox_Preview.Text += $", prob={item.Probability.ToString().Replace(',', '.')} ";
                if (item.HasFillCondition) richTextBox_Preview.Text += $", cond={item.Condition.ToString().Replace(',', '.')} ";
                richTextBox_Preview.Text += " \\n" + Environment.NewLine;
            }
            foreach (var include in character.Supplies.Includes)
            {
                richTextBox_Preview.Text += $"#include \"{include}\"" + Environment.NewLine;
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

            var selectedIndexListBox = listBox_Supplies.SelectedIndex == -1 ? 0 : listBox_Supplies.SelectedIndex;
            var selectedItem = _selectedCharacter.Supplies.Items.SingleOrDefault(supply => supply.Name == listBox_Supplies.SelectedItem.ToString());
            if (selectedItem is not null)
                _selectedCharacter.Supplies.Items.Insert(_selectedCharacter.Supplies.Items.IndexOf(selectedItem), newSupply);
            else
                _selectedCharacter.Supplies.Items.Add(newSupply);
            ReFillSupplies(_selectedCharacter.Supplies.Items.Select(supply => supply.Name));
            ReFillPreview(_selectedCharacter);
            listBox_Supplies.SelectedIndex = selectedIndexListBox;
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

            var selectedIndexListBox = listBox_Supplies.SelectedIndex;
            _selectedCharacter.Supplies.Items[oldSupplyIndex] = newSupply;
            ReFillSupplies(_selectedCharacter.Supplies.Items.Select(item => item.Name));
            ReFillPreview(_selectedCharacter);
            listBox_Supplies.SelectedIndex = selectedIndexListBox;
        }
        private void button_Delete_Supply_Click(object sender, EventArgs e)
        {
            if (comboBox_Selected_Character.SelectedIndex == -1)
            {
                MessageBox.Show("Персонаж для удаления предмета не выбран", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listBox_Supplies.SelectedIndex == -1)
            {
                MessageBox.Show("Предмет для удаления не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var deletedSupplyName = listBox_Supplies.SelectedItem.ToString();
            var recivedSupply = _selectedCharacter.Supplies.Items.SingleOrDefault(item => item.Name == deletedSupplyName);
            if (recivedSupply is null)
            {
                MessageBox.Show("Данного предмета не существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedIndexListBox = listBox_Supplies.Items.Count == listBox_Supplies.SelectedIndex + 1 ? 0 : listBox_Supplies.SelectedIndex;
            selectedIndexListBox = listBox_Supplies.Items.Count == 1 ? -1 : selectedIndexListBox;
            var recivedRemoveSupply = _selectedCharacter.Supplies.Items.SingleOrDefault(item => item.Name == deletedSupplyName);
            _selectedCharacter.Supplies.Items.Remove(recivedRemoveSupply);
            ReFillSupplies(_selectedCharacter.Supplies.Items.Select(item => item.Name));
            ReFillPreview(_selectedCharacter);
            listBox_Supplies.SelectedIndex = selectedIndexListBox;
        }
        private Supply CreateSupplyFromInputs()
        {
            var addons = new List<string>();
            foreach (var addon in listBox_Addons.Items)
                addons.Add(addon.ToString());

            var condition = 1.0;
            var probability = 1.0;

            if (!string.IsNullOrEmpty(textBox_Supply_Condition.Text))
                condition = Convert.ToDouble(textBox_Supply_Condition.Text.Replace('.', ','));
            if (!string.IsNullOrEmpty(textBox_Supply_Probability.Text))
                probability = Convert.ToDouble(textBox_Supply_Probability.Text.Replace('.', ','));

            var supply = new Supply(
                        textBox_Supply_Name.Text,
                        addons,
                        checkBox_Supply_Condition.Checked,
                        checkBox_Supply_Probability.Checked,
                        probability,
                        condition,
                        Convert.ToInt32(textBox_Supply_Count.Text)
                    );

            return supply;
        }
        private bool CheckInputsForSupply()
        {
            try
            {
                Convert.ToInt32(textBox_Supply_Count.Text);
                if (!string.IsNullOrEmpty(textBox_Supply_Probability.Text) && checkBox_Supply_Probability.Checked)
                    Convert.ToDouble(textBox_Supply_Probability.Text.Replace('.', ','));
                if (!string.IsNullOrEmpty(textBox_Supply_Condition.Text) && checkBox_Supply_Condition.Checked)
                    Convert.ToDouble(textBox_Supply_Condition.Text.Replace('.', ','));
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
            if (string.IsNullOrEmpty(addonName))
            {
                MessageBox.Show("Поле для имени аддона не заполнено", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_selectedSupply.Addons.Any(addon => addon == addonName))
            {
                MessageBox.Show("Данный аддон уже есть у предмета", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedIndexListBox = listBox_Addons.SelectedIndex == -1 ? 0 : listBox_Addons.SelectedIndex;
            var selectedItem = _selectedSupply.Addons.SingleOrDefault(addon => addon == listBox_Addons.SelectedItem.ToString());
            if (selectedItem is not null)
                _selectedSupply.Addons.Insert(_selectedSupply.Addons.IndexOf(selectedItem), addonName);
            else
                _selectedSupply.Addons.Add(addonName);
            ReFillAddons(_selectedSupply.Addons);
            ReFillPreview(_selectedCharacter);
            listBox_Addons.SelectedIndex = selectedIndexListBox;
        }
        private void button_Change_Addon_Click(object sender, EventArgs e)
        {
            if (listBox_Supplies.SelectedIndex == -1)
            {
                MessageBox.Show("Предмет для изменения аддона не выбран", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var addonName = textBox_Addon_Name.Text;
            if (listBox_Addons.SelectedIndex == -1)
            {
                MessageBox.Show("Аддон для изменения не выбран", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(addonName))
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

            var selectedIndexListBox = listBox_Addons.SelectedIndex;
            _selectedSupply.Addons[selectedAddonIndex] = addonName;
            ReFillAddons(_selectedSupply.Addons);
            ReFillPreview(_selectedCharacter);
            listBox_Addons.SelectedIndex = selectedIndexListBox;
        }
        private void button_Delete_Addon_Click(object sender, EventArgs e)
        {
            if (listBox_Supplies.SelectedIndex == -1)
            {
                MessageBox.Show("Предмет для удаления аддона не выбран", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listBox_Addons.SelectedIndex == -1)
            {
                MessageBox.Show("Аддон для удаления не выбран", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var addonName = listBox_Addons.SelectedItem.ToString();
            if (!_selectedSupply.Addons.Any(addon => addon == addonName))
            {
                MessageBox.Show("Данного аддона нет у предмета", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedIndexListBox = listBox_Addons.Items.Count == listBox_Addons.SelectedIndex + 1 ? 0 : listBox_Addons.SelectedIndex;
            selectedIndexListBox = listBox_Addons.Items.Count == 1 ? -1 : selectedIndexListBox;
            var recivedRemoveSupply = _selectedSupply.Addons.SingleOrDefault(addon => addon == addonName);
            _selectedSupply.Addons.Remove(addonName);
            ReFillAddons(_selectedSupply.Addons);
            ReFillPreview(_selectedCharacter);
            listBox_Addons.SelectedIndex = selectedIndexListBox;
            textBox_Addon_Name.Text = string.Empty;
            textBox_Addon_Name.Text = listBox_Addons.SelectedIndex != -1 ? listBox_Addons.SelectedItem.ToString() : "";
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
            if (string.IsNullOrEmpty(includePath))
            {
                MessageBox.Show("Поле для пути модуля не заполнено", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_selectedCharacter.Supplies.Includes.Any(include => include == includePath))
            {
                MessageBox.Show("Данный модуль уже существует", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedIndexListBox = listBox_Includes.SelectedIndex == -1 ? 0 : listBox_Includes.SelectedIndex;
            var selectedItem = _selectedCharacter.Supplies.Includes.SingleOrDefault(include => include == listBox_Includes.SelectedItem.ToString());
            if (selectedItem is not null)
                _selectedCharacter.Supplies.Includes.Insert(_selectedCharacter.Supplies.Includes.IndexOf(selectedItem), includePath);
            else
                _selectedCharacter.Supplies.Includes.Add(includePath);
            ReFillIncludes(_selectedCharacter.Supplies.Includes);
            ReFillPreview(_selectedCharacter);
            listBox_Includes.SelectedIndex = selectedIndexListBox;
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
            if (string.IsNullOrEmpty(includePath))
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

            var selectedIndexListBox = listBox_Includes.SelectedIndex;
            _selectedCharacter.Supplies.Includes[selectedIncludeIndex] = includePath;
            ReFillIncludes(_selectedCharacter.Supplies.Includes);
            ReFillPreview(_selectedCharacter);
            listBox_Includes.SelectedIndex = selectedIncludeIndex;
        }
        private void button_Delete_Include_Click(object sender, EventArgs e)
        {
            if (comboBox_Selected_Character.SelectedIndex == -1)
            {
                MessageBox.Show("Персонаж для удаления модуля не выбран", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listBox_Includes.SelectedIndex == -1)
            {
                MessageBox.Show("Путь модуля не выбран", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var includePath = listBox_Includes.SelectedItem.ToString();
            if (!_selectedCharacter.Supplies.Includes.Any(include => include == includePath))
            {
                MessageBox.Show("Данного модуля не существует", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedIndexListBox = listBox_Includes.Items.Count == listBox_Includes.SelectedIndex + 1 ? 0 : listBox_Includes.SelectedIndex;
            selectedIndexListBox = listBox_Includes.Items.Count == 1 ? -1 : selectedIndexListBox;
            var recivedRemoveSupply = _selectedCharacter.Supplies.Includes.SingleOrDefault(include => include == includePath);
            _selectedCharacter.Supplies.Includes.Remove(includePath);
            ReFillIncludes(_selectedCharacter.Supplies.Includes);
            ReFillPreview(_selectedCharacter);
            listBox_Includes.SelectedIndex = selectedIndexListBox;
            textBox_Include.Text = listBox_Includes.SelectedIndex != -1 ? listBox_Includes.SelectedItem.ToString() : "";
        }
        #endregion
    }
}

