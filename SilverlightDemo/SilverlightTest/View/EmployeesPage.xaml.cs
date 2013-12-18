using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Matrix.Silverlight.Forms;
using Matrix.Silverlight.Net;
using SilverlightTest.EmployeesService;
using SilverlightTest.Exceptions;


namespace SilverlightTest.View
{
    public partial class Employees : UserControl
    {
        public static ObservableCollection<Models.EmployeeInfo> DataList;
        private ObservableCollection<Models.EmployeeInfo> _selectedItems;
        public static ModalWin modalWindow;

        public Employees()
        {
            InitializeComponent();
            DataList = new ObservableCollection<Models.EmployeeInfo>();
            _selectedItems = new ObservableCollection<Models.EmployeeInfo>();
            GetEmployess();
            GetDepartment();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        private void GetEmployess()
        {
            InvokeUtils.ThreadProc(this, () =>
            {
                ObservableCollection<Models.EmployeeInfo> list = Models.EmployeeInfo.GetEmployees();
                InvokeUtils.SyncInvoke(this, () => { this.Dgrid.ItemsSource = list; DataList = CloneList(list); });
            });
        }

        /// <summary>
        /// 获取部门
        /// </summary>
        private void GetDepartment()
        {
            InvokeUtils.ThreadProc(this, () =>
            {
                ObservableCollection<Models.DepartmentInfo> list = Models.DepartmentInfo.GetDepartment();

                InvokeUtils.SyncInvoke(this, () =>
                {
                    list.Add(new Models.DepartmentInfo() { Id = 3, Name = "All" });
                    this.DepartmentElement.ItemsSource = list;
                    this.DepartmentElement.DisplayMemberPath = "Name";
                    this.DepartmentElement.SelectedIndex = list.Count - 1;
                });
            });
        }

        /// <summary>
        /// CloneList
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private ObservableCollection<Models.EmployeeInfo> CloneList(ObservableCollection<Models.EmployeeInfo> list)
        {
            ObservableCollection<Models.EmployeeInfo> resList = new ObservableCollection<Models.EmployeeInfo>();
            foreach (var item in list)
            {
                Models.EmployeeInfo info = new Models.EmployeeInfo();
                info.Birthday = item.Birthday;
                info.Bonus = item.Bonus;
                info.DepartmentId = item.DepartmentId;
                info.DepartmentName = item.DepartmentName;
                info.Id = item.Id;
                info.Level = item.Level;
                info.Name = item.Name;
                info.Remark = item.Remark;
                info.Salary = item.Salary;
                info.Sex = item.Sex;

                ObservableCollection<Models.SkillInfo> sk = new ObservableCollection<Models.SkillInfo>();
                if (item.Skills != null)
                {
                    foreach (var i in item.Skills)
                    {
                        info.Skills.Add(i);
                    }
                }
                info.Skills = sk;
                resList.Add(info);
            }
            return resList;
        }

        /// <summary>
        /// 筛选部门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ObservableCollection<Models.EmployeeInfo> list = new ObservableCollection<Models.EmployeeInfo>();
            if (this.DepartmentElement.SelectedIndex == 3)
            {
                this.Dgrid.ItemsSource = DataList;
                return;
            }
            foreach (var item in DataList)
            {
                if (item.DepartmentId == this.DepartmentElement.SelectedIndex)
                {
                    list.Add(item);
                }
            }
            this.Dgrid.ItemsSource = list;
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCommitAll(object sender, RoutedEventArgs e)
        {
            _selectedItems.Clear();
            foreach (Models.EmployeeInfo obj in Dgrid.ItemsSource as ObservableCollection<Models.EmployeeInfo>)
            {
                if (Dgrid.Columns != null || Dgrid.Columns.Count != 0)
                {
                    CheckBox cb = Dgrid.Columns[0].GetCellContent(obj) as CheckBox;
                    if (cb != null)
                    {
                        if (cb.IsChecked == false)
                        {
                            cb.IsChecked = true;
                            _selectedItems.Add((Models.EmployeeInfo)obj);
                        }
                    }
                }
            }

        }

        /// <summary>
        /// 取消全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCancleSelect(object sender, RoutedEventArgs e)
        {
            foreach (Models.EmployeeInfo obj in Dgrid.ItemsSource as ObservableCollection<Models.EmployeeInfo>)
            {
                if (Dgrid.Columns != null)
                {
                    CheckBox cb = Dgrid.Columns[0].GetCellContent(obj) as CheckBox;
                    if (cb != null)
                        cb.IsChecked = false;
                }
            }
            _selectedItems.Clear();

        }

        /// <summary>
        /// 单选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCheckClick(object sender, RoutedEventArgs e)
        {
            CheckBox cb = Dgrid.Columns[0].GetCellContent(Dgrid.SelectedItem) as CheckBox;
            if (cb.IsChecked == true)
            {
                _selectedItems.Add((Models.EmployeeInfo)Dgrid.SelectedItem);
            }
            else
            {
                _selectedItems.Remove((Models.EmployeeInfo)Dgrid.SelectedItem);
            }
        }


        private void OnEdit(object sender, RoutedEventArgs e)
        {
            Models.EmployeeInfo emp = this.Dgrid.SelectedItem as Models.EmployeeInfo;
            modalWindow = new ModalWin(emp);
            modalWindow.Title = "编辑员工窗口";
            modalWindow.Tag = "edit";
            modalWindow.Closed += (s1, e1) =>
            {
                if (modalWindow.DialogResult == true)
                    GetEmployess();
            };
            modalWindow.Show();


        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {

            if (_selectedItems.Count != 0)
            {
                ObservableCollection<int> delList = new ObservableCollection<int>();
                if (_selectedItems.Count > 1)
                {
                    throw new CustomException() { ExceptionMessage = "请勿多选删除行...." };
                }
                for (int i = 0; i < _selectedItems.Count; i++)
                {
                    delList.Add(_selectedItems[i].Id);
                }
                InvokeUtils.ThreadProc(this, () =>
                {
                    WaittingDialog.Show(this);
                    Models.EmployeeInfo.Delete(delList);
                    InvokeUtils.SyncInvoke(this, () => { GetEmployess(); this.DepartmentElement.SelectedIndex = 3; });
                });
            }
            else
            {
                MsgBox.Info(this, "选择要删除的行.!");
            }
        }

        private void OnCreate(object sender, RoutedEventArgs e)
        {
            var emp = new Models.EmployeeInfo();
            byte[] buffer = Guid.NewGuid().ToByteArray();
            emp.Id = BitConverter.ToInt32(buffer, 0);
            emp.Birthday = DateTime.Now.AddYears(-20);
            modalWindow = new ModalWin(emp);
            modalWindow.Title = "添加员工窗口";
            modalWindow.Tag = "add";
            modalWindow.Closed += (s1, e1) =>
            {
                if (modalWindow.DialogResult == true)
                    GetEmployess();
            };
            modalWindow.Show();


        }




    }
}
