﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Nursery_Management_System_WPF
{
    /// <summary>
    /// Interaction logic for staffWindow.xaml
    /// </summary>
    public partial class staffWindow : Window
    {
        public staffWindow()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void roomButton_Click(object sender, RoutedEventArgs e)
        {
            //hide all other windows
            this.profile.Visibility = Visibility.Hidden;
            //show room grid
            this.room.Visibility = Visibility.Visible;
        }

        private void staffProfileButton_Click(object sender, RoutedEventArgs e)
        {
            //fill textbox with data
            SQLQuery mSQLQuery = new SQLQuery();

            firstName.Text = GlobalVariables.globalStaff.firstName;
            lastName.Text = GlobalVariables.globalStaff.lastName;
            DataTable dt = mSQLQuery.selectUsernameByIDAndType(Convert.ToInt64(GlobalVariables.globalStaff.id), "Staff");

            username.Text = dt.Rows[0]["userName"].ToString();
            password.Password = dt.Rows[0]["userPassword"].ToString();

            email.Text = GlobalVariables.globalStaff.email;
            phoneNumber.Text = GlobalVariables.globalStaff.phoneNumber;
            ID.Text = (GlobalVariables.globalStaff.id).ToString();

            //hide all other windows
            this.room.Visibility = Visibility.Hidden;
            //show profile grid
            this.profile.Visibility = Visibility.Visible;
        }

        private void signOutButton_Click(object sender, RoutedEventArgs e)
        {
            signIn logIn = new signIn();
            logIn.Show();
            this.Close();
        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void editProfileButton_Click(object sender, RoutedEventArgs e)
        {
            SQLQuery mSQLQuery = new SQLQuery();
            
            if (checkEnteredData())
            {
                GlobalVariables.globalStaff.id = Convert.ToInt64(ID.Text);
                GlobalVariables.globalStaff.firstName = firstName.Text;
                GlobalVariables.globalStaff.lastName = lastName.Text;

                mSQLQuery.updateUsername(Convert.ToInt64(ID.Text), "Staff", username.Text, password.Password);

                GlobalVariables.globalStaff.email = email.Text;
                GlobalVariables.globalStaff.phoneNumber = phoneNumber.Text;
                
                mSQLQuery.updateStaffData(GlobalVariables.globalStaff);
                MessageBox.Show("Data Updated sucessfuly !", "Process Finshed", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public bool checkEnteredData()
        {
            bool ans = true;
            ValidateData validator = new ValidateData();
            SQLQuery mSql = new SQLQuery();

            if (!validator.verifyField(firstName.Text) || firstName.Text.Equals("Enter First Name Here"))
            {
                ans = false;
                MessageBox.Show("Please Correct Your First Name !", "Error Occur", MessageBoxButton.OK, MessageBoxImage.Hand);
                firstNameError.Visibility = Visibility.Visible;
            }
            else
            {
                firstNameError.Visibility = Visibility.Hidden;
            }

            if (!validator.verifyField(lastName.Text) || lastName.Text.Equals("Enter Last Name Here"))
            {
                ans = false;
                MessageBox.Show("Please Correct Your Last Name !", "Error Occur", MessageBoxButton.OK, MessageBoxImage.Hand);
                lastNameError.Visibility = Visibility.Visible;
            }
            else
            {
                lastNameError.Visibility = Visibility.Hidden;
            }

            if (!validator.checkNationalID(ID.Text))
            {
                ans = false;
                MessageBox.Show("Please Correct Your ID !", "Error Occur", MessageBoxButton.OK, MessageBoxImage.Hand);
                IDError.Visibility = Visibility.Visible;
            }
            else
            {
                IDError.Visibility = Visibility.Hidden;
            }

            if (!validator.checkMails(email.Text))
            {
                ans = false;
                MessageBox.Show("Please Correct Your Email !", "Error Occur", MessageBoxButton.OK, MessageBoxImage.Hand);
                emailError.Visibility = Visibility.Visible;
            }
            else
            {
                emailError.Visibility =Visibility.Hidden;
            }

            if (!validator.checkPhoneNum(phoneNumber.Text))
            {
                ans = false;
                MessageBox.Show("Please Correct Your Phone Number !", "Error Occur", MessageBoxButton.OK, MessageBoxImage.Hand);
                phoneError.Visibility = Visibility.Visible;

            }
            else
            {
                phoneError.Visibility = Visibility.Hidden;
            }

            if (mSql.checkForUsername(username.Text) || username.Text.Equals("Enter Username Here"))
            {
                ans = false;
                MessageBox.Show("Please Correct Your UserName !", "Error Occur", MessageBoxButton.OK, MessageBoxImage.Hand);

                usernameError.Visibility = Visibility.Visible;
            }
            else
            {
               usernameError.Visibility = Visibility.Hidden;
            }
            
            if (!validator.verifyField(password.Password))
            {
                ans = false;
                MessageBox.Show("Please Correct Your Password !", "Error Occur", MessageBoxButton.OK, MessageBoxImage.Hand);

               passwordError.Visibility = Visibility.Visible;
            }
            else
            {
               passwordError.Visibility = Visibility.Hidden;
            }
            
            return ans;
        }

    }
}
