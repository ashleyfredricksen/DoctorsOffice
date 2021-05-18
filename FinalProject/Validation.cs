using System;
using System.Windows.Forms;

namespace FinalProject
{
    public class Validation
    {
        public bool IsPresent(TextBox textBox, string name) //checks to see if anything is entered in textboxes
        {
            if (textBox.Text == "")
            {
                MessageBox.Show(name + " is a required field.", "Entry Error");
                textBox.Focus();
                return false;
            }
            return true;
        }

        public bool IsDateTime(TextBox textBox, string name) //checks to see if dob is valid date
        {
            DateTime dob;
            if (DateTime.TryParse(textBox.Text, out dob))
            {
                return true;
            }
            else
            {
                MessageBox.Show(name + " is not a valid date.", "Entry Error");
                textBox.Focus();
                return false;
            }
        }

        public bool IsWithinRange(TextBox textBox, string name, DateTime min, DateTime max) //checks if birthday is within range of dates
        {
            DateTime birthday = Convert.ToDateTime(textBox.Text);
            if (birthday < min || birthday > max)
            {
                MessageBox.Show(name + " cannot be before " + min.ToShortDateString() + " or after " + max.ToShortDateString() + ".", "Entry Error");
                textBox.Focus();
                return false;
            }
            return true;
        }

        public bool ValidDoctor(TextBox textBox, string name) // checks if doctor name entered is valid
        {
            string docName = textBox.Text;
            if (docName == "Lee" || docName == "Harper" || docName == "Johnson" || docName == "Daniels")               
                return true;
            else
            {
                MessageBox.Show("The " + name + " entered is not valid. Please try again.", "Entry Error");
                textBox.Focus();
                return false;
            }
        }

        public bool IsInteger(TextBox textBox, string name) //checks if patientid is an integer
        {
            int ptid;
            if (Int32.TryParse(textBox.Text, out ptid))            
                return true;            
            else
            {
                MessageBox.Show(name + " must be a valid integer.", "Entry Error");
                return false;
            }
        }
    }
}
