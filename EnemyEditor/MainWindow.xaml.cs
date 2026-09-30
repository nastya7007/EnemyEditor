using System;
using System.Windows;
using System.Windows.Controls;

namespace EnemyEditor
{
    public partial class MainWindow : Window
    {
        CEnemyTemplateList enemyList;

        public MainWindow()
        {
            InitializeComponent();
            enemyList = new CEnemyTemplateList();
        }

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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // позже
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            // позже
        }

        private void LoadIconsButton_Click(object sender, RoutedEventArgs e)
        {
            // позже
        }

        private void IconsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // позже
        }

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