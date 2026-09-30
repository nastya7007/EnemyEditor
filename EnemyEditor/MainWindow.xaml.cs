using System;
using System.Collections.Generic;
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
                double lifeModifier = double.Parse(LifeModifierBox.Text);
                double goldModifier = double.Parse(GoldModifierBox.Text);
                double spawnChance = double.Parse(SpawnChanceBox.Text);

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
            }
        }

        // --- Save / Load (заглушки) ---

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // позже
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            // позже
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
            // позже
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