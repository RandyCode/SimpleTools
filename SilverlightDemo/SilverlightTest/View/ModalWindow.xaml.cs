using Matrix.Silverlight.Forms;
using Matrix.Silverlight.Validation;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SilverlightTest.View
{
    public partial class ModalWin : ChildWindow
    {
        private Models.EmployeeInfo _modle = new Models.EmployeeInfo();
        private Models.EmployeeInfo _oldModle = new Models.EmployeeInfo();
        private int _id;

        public ModalWin(Models.EmployeeInfo emp)
        {
            InitializeComponent();
            GetDepartment();
            GetAllSkills();

            this._modle = emp;
            if (emp.Skills == null)
                _modle.Skills = new ObservableCollection<Models.SkillInfo>();
            CloneModle(emp);
            LoadMySkill();
            EmployeeGrid.DataContext = _modle;
            this.LevelSlider.Value = this._modle.Level;
            _id = _modle.Id;
        }

        private void GetDepartment()
        {
            InvokeUtils.ThreadProc(this, () =>
            {
                ObservableCollection<Models.DepartmentInfo> list = Models.DepartmentInfo.GetDepartment();
                InvokeUtils.SyncInvoke(this, () =>
                {
                    this.DepartmentComboBox.ItemsSource = list;
                    this.DepartmentComboBox.DisplayMemberPath = "Name";
                    this.DepartmentComboBox.SelectedIndex = _modle.DepartmentId;
                });
            });
        }

        private void GetAllSkills()
        {
            InvokeUtils.ThreadProc(this, () =>
            {
                ObservableCollection<Models.SkillInfo> list = Models.SkillInfo.GetSKills();
                InvokeUtils.SyncInvoke(this, () =>
                {
                    this.AllSkillsElement.ItemsSource = list;
                    this.AllSkillsElement.DisplayMemberPath = "Info";
                });
            });
        }

        private void CloneModle(Models.EmployeeInfo emp)
        {
            _oldModle.Id = emp.Id;
            _oldModle.Level = emp.Level;
            _oldModle.Name = emp.Name;
            _oldModle.Remark = emp.Remark;
            _oldModle.Salary = emp.Salary;
            _oldModle.Sex = emp.Sex;
            _oldModle.Skills = new ObservableCollection<Models.SkillInfo>();
            foreach (Models.SkillInfo sk in emp.Skills)
            {
                _oldModle.Skills.Add(sk);
            }
            _oldModle.Birthday = emp.Birthday;
            _oldModle.Bonus = emp.Bonus;
            _oldModle.DepartmentId = emp.DepartmentId;
            _oldModle.DepartmentName = emp.DepartmentName;
        }

        private void OnCommit(object sender, RoutedEventArgs e)
        {
            EditEmployee();
            ValidationToolkit toolkit = new ValidationToolkit(this);
            if (toolkit.Validate() == false)
            {
                return;
            }


            if (this.Tag.ToString() == "add")
            {
                InvokeUtils.ThreadProc(this, () =>
                {
                    WaittingDialog.Show(this);
                    Models.EmployeeInfo.Add(_modle);
                    InvokeUtils.SyncInvoke(this, () =>
                    {
                        this.DialogResult = true;
                        this.Close();
                    });
                });
            }
            else
            {
                InvokeUtils.ThreadProc(this, () =>
                {
                    WaittingDialog.Show(this);
                    Models.EmployeeInfo.Modify(_modle);
                    InvokeUtils.SyncInvoke(this, () =>
                    {
                        this.DialogResult = true;
                        this.Close();
                    });
                });
            }
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            if (this.Tag.ToString() == "edit")
            {
                InvokeUtils.ThreadProc(this, () =>
                {
                    Models.EmployeeInfo.Modify(_oldModle);
                    InvokeUtils.SyncInvoke(this, () =>
                    {
                        this.DialogResult = true;
                        this.Close();
                    });
                });
            }
            this.DialogResult = false;
        }

        private void LoadMySkill()
        {
            if (_modle.Skills.Count != 0)
            {
                foreach (var skill in _modle.Skills)
                {
                    MySkillsElement.Items.Add(skill);
                }
            }
        }

        private void EditEmployee()
        {
            _modle.Remark = this.RemarkElement.Text;
            _modle.Level = (int)this.LevelSlider.Value;
            _modle.Name = this.EmplpyeeName.Text.ToString();
            _modle.Salary = this.SalaryElement.Value;
            _modle.Bonus = this.BounsElement.Value;
            _modle.Birthday = (DateTime)this.BirthdayElement.SelectedDate;
            _modle.DepartmentId = this.DepartmentComboBox.SelectedIndex;
            _modle.Sex = ((bool)this.FemaleElement.IsChecked ? true : false);
            _modle.DepartmentName = (this.DepartmentComboBox.SelectedItem as Models.DepartmentInfo).Name;

            ObservableCollection<Models.SkillInfo> skill = new ObservableCollection<Models.SkillInfo>();
            ItemCollection items = MySkillsElement.Items;
            foreach (Models.SkillInfo item in items)
            {
                skill.Add(item);
            }

            _modle.Skills = skill;

        }

        private void OnRemoveSkill(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                foreach (Models.SkillInfo item in MySkillsElement.Items)
                {

                    if (item.Id == (int)btn.Tag)
                    {
                        _modle.Skills.Remove(item);
                        break;
                    }
                }
            }

        }

        private void OnSelectSkill(object sender, RoutedEventArgs e)
        {
            var CurrentSkill = AllSkillsElement.SelectedItem as Models.SkillInfo;
            bool flag = false;
            foreach (Models.SkillInfo item in MySkillsElement.Items)
            {

                if (item.Id == CurrentSkill.Id)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                _modle.Skills.Add(CurrentSkill);
            }
        }

        private void OnvalidateName(object sender, Matrix.Silverlight.Validation.ValidationEventArgs e)
        {
            var firstLetter = this.EmplpyeeName.Text.Substring(0, 1);
            e.Result = firstLetter.Equals(firstLetter.ToUpper(), StringComparison.CurrentCulture);
        }

        private void OnValidateNameInThread(object sender, Matrix.Silverlight.Validation.ValidationEventArgs e)
        {
            foreach (var item in Employees.DataList)
            {
                if (item.Name == this.EmplpyeeName.Text && _id != item.Id)
                {
                    e.Result = false;
                    return;
                }
            }

        }



    }

}

