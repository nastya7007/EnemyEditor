using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace EnemyEditor
{
    public partial class MainWindow : Window
    {
        CEnemyTemplateList enemyList;
        List<EnemyIcon> enemyIcons = new List<EnemyIcon>();

        public MainWindow()
        {
            InitializeComponent();
            enemyList = new CEnemyTemplateList();
        }

        // --- Add / Remove ---

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = EnemyNameBox.Text;
                string iconName = IconNameBox.Text;
                int baseLife = int.Parse(BaseLifeBox.Text);
                int baseGold = int.Parse(BaseGoldBox.Text);

                double lifeModifier = double.Parse(LifeModifierBox.Text, CultureInfo.InvariantCulture);
                double goldModifier = double.Parse(GoldModifierBox.Text, CultureInfo.InvariantCulture);
                double spawnChance = double.Parse(SpawnChanceBox.Text, CultureInfo.InvariantCulture);

                enemyList.AddEnemy(name, iconName, baseLife, lifeModifier,
                    baseGold, goldModifier, spawnChance);

                UpdateEnemiesListBox();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении: " + ex.Message);
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (EnemiesListBox.SelectedIndex >= 0)
            {
                enemyList.DeleteEnemyByIndex(EnemiesListBox.SelectedIndex);
                UpdateEnemiesListBox();
                ClearInputFields();
            }
        }

        // --- Save / Load ---

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "JSON файлы (*.json)|*.json";
            dialog.FileName = "enemies.json";

            if (dialog.ShowDialog() == true)
            {
                enemyList.SaveToJson(dialog.FileName);
                MessageBox.Show("Сохранено!");
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JSON файлы (*.json)|*.json";

            if (dialog.ShowDialog() == true)
            {
                enemyList.LoadFromJson(dialog.FileName);
                UpdateEnemiesListBox();
                MessageBox.Show("Загружено!");
            }
        }

        // --- Иконки ---

        private void LoadIconsButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog dialog = new OpenFolderDialog();
            dialog.Title = "Выберите папку с иконками";

            if (dialog.ShowDialog() == true)
            {
                LoadIconsFromFolder(dialog.FolderName);
                MessageBox.Show("Загружено иконок: " + enemyIcons.Count);
            }
        }

        private void LoadIconsFromFolder(string path)
        {
            enemyIcons.Clear();
            IconsListBox.Items.Clear();

            string[] files = Directory.GetFiles(path, "*.png", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                EnemyIcon icon = new EnemyIcon
                {
                    Name = System.IO.Path.GetFileName(file),
                    ImagePath = file
                };
                enemyIcons.Add(icon);

                Image image = new Image()
                {
                    Source = new BitmapImage(new Uri(icon.ImagePath)),
                    Height = 64
                };

                IconsListBox.Items.Add(image);
            }
        }

        private void IconsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IconsListBox.SelectedItem is Image selectedImage)
            {
                MainEnemyIcon.Source = selectedImage.Source;

                string fullPath = selectedImage.Source.ToString();
                string iconName = System.IO.Path.GetFileName(fullPath);

                IconNameBox.Text = iconName;
            }
        }

        // --- Выбор врага в списке ---

        private void EnemiesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EnemiesListBox.SelectedIndex < 0)
                return;

            CEnemyTemplate enemy = enemyList.GetEnemyByIndex(EnemiesListBox.SelectedIndex);

            if (enemy != null)
            {
                EnemyNameBox.Text = enemy.Name;
                IconNameBox.Text = enemy.IconName;
                BaseLifeBox.Text = enemy.BaseLife.ToString();
                BaseGoldBox.Text = enemy.BaseGold.ToString();
                LifeModifierBox.Text = enemy.LifeModifier.ToString(CultureInfo.InvariantCulture);
                GoldModifierBox.Text = enemy.GoldModifier.ToString(CultureInfo.InvariantCulture);
                SpawnChanceBox.Text = enemy.SpawnChance.ToString(CultureInfo.InvariantCulture);
            }
        }

        // --- Вспомогательные ---

        private void UpdateEnemiesListBox()
        {
            EnemiesListBox.Items.Clear();
            foreach (string name in enemyList.GetListOfEnemyNames())
            {
                EnemiesListBox.Items.Add(name);
            }
        }

        private void ClearInputFields()
        {
            EnemyNameBox.Text = "";
            IconNameBox.Text = "";
            BaseLifeBox.Text = "";
            BaseGoldBox.Text = "";
            LifeModifierBox.Text = "";
            GoldModifierBox.Text = "";
            SpawnChanceBox.Text = "";
        }
    }
}