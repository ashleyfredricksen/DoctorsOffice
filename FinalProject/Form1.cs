/* Ashley Fredricksen
 * March 28, 2019
 * Final Project
 */
using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FinalProject
{
    public partial class Form1 : Form
    {     
        public Form1()
        {
            InitializeComponent();
        }

        // arrays for payment type and appointment type drop downs
        string[] doctors = { "Lee", "Harper", "Johnson", "Daniels" };
        string[] payment = { "Cash", "Credit Card", "Insurance" };
        string[] appointment = { "Routine checkup", "Sick visit" };

        // variables to be used throughout
        int docID, payID, appID;        
        string first, middle, last, docName, appType, payType;       
        public DateTime dob;
        
        Validation valid = new Validation();

        public void PatientInput()  // gathers patient input to be inserted into database
        {
            first = txtFirstName.Text;
            middle = txtMiddle.Text;
            last = txtLast.Text;
            dob = Convert.ToDateTime(txtDOB.Text);
            docName = docLastName.Text;
            appType = cboAppt.Text;
            payType = cboPayment.Text;

            //assigns id based on name
            if (docLastName.SelectedIndex == 0)
            {
                docID = 1;
            }
            if (docLastName.SelectedIndex == 1)
            {
                docID = 2;
            }
            if (docLastName.SelectedIndex == 2)
            {
                docID = 3;
            }
            if (docLastName.SelectedIndex == 3)
            {
                docID = 4;
            }

            //assigns id based on payment type
            if (cboPayment.SelectedIndex == 0)
            {
                payID = 1;
            }
            if (cboPayment.SelectedIndex == 1)
            {
                payID = 2;
            }
            if (cboPayment.SelectedIndex == 2)
            {
                payID = 3;
            }

            //assigns id based on appointment type
            if (cboAppt.SelectedIndex == 0)
            {
                appID = 1;
            }
            if (cboAppt.SelectedIndex == 1)
            {
                appID = 2;
            }
        }

        // establishes connection to database and inserts patient info into it
        private void Connection()
        {
            PatientInput();
            
            string connectionString = "Data Source=localhost; Initial Catalog=DoctorsOffice; Integrated Security=True";

            string insert = "INSERT INTO dbo.PatientInfo(FirstName, MiddleName, LastName, DOB, DoctorID, PaymentID, AppointmentID) VALUES(@FirstName, @MiddleName, @LastName, @DOB, @DoctorID, @PaymentID, @AppointmentID)";
          
            // these strings insert info into the doctorinfo, appointmentinfo, and paymentinfo tables.  
            // string insertDI = "INSERT INTO dbo.DoctorInfo(DoctorID, DoctorName) VALUES(@DoctorID, @DoctorName)";
           // string insertApp = "INSERT INTO dbo.AppointmentInfo(AppointmentID, Description) VALUES(@AppointmentID, @Description)";
           // string insertPay = "INSERT INTO dbo.PaymentInfo(PaymentID, Description) VALUES(@PaymentID, @PDescription)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command2 = new SqlCommand(insert, connection);
                command2.Parameters.AddWithValue("@FirstName", first.Trim());
                command2.Parameters.AddWithValue("@MiddleName", middle.Trim());
                command2.Parameters.AddWithValue("@LastName", last.Trim());
                command2.Parameters.Add("@DOB", SqlDbType.DateTime2).Value = dob;
                command2.Parameters.AddWithValue("@DoctorID", docID);
                command2.Parameters.AddWithValue("@PaymentID", payID);
                command2.Parameters.AddWithValue("@AppointmentID", appID);

                // these commands were only used to add ID info for the doctors, appointment and payment types.
                //  SqlCommand command = new SqlCommand(insertDI, connection);                
                //  SqlCommand command3 = new SqlCommand(insertApp, connection);
                //  SqlCommand command4 = new SqlCommand(insertPay, connection);
                //  command3.Parameters.AddWithValue("@Description", appType);                
                //  command4.Parameters.AddWithValue("@PDescription", payType);                

                try
                {
                    connection.Open();
                    command2.ExecuteNonQuery();

                    // executed the commands for ID information for the additional tables
                    // command.ExecuteNonQuery();
                    // command3.ExecuteNonQuery();
                    // command4.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        // references the validation class and assigns the relevant arguments
        public bool IsValidData()
        {                                             
            return
                valid.IsPresent(txtFirstName, "First Name") &&
                
                valid.IsPresent(txtLast, "Last Name") &&

               // valid.IsPresent(txtDoctor, "Doctor Name") &&
               // valid.ValidDoctor(txtDoctor, "doctor name") &&

                valid.IsPresent(txtDOB, "Date of Birth") &&
                valid.IsDateTime(txtDOB, "Date of Birth") &&
                valid.IsWithinRange(txtDOB, "Date of Birth", DateTime.Today.AddYears(-150), DateTime.Today);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsValidData() == true) //checks to see if data is valid before attempting to add to database
            {
                Connection();
                MessageBox.Show("Patient successfully added.", "Alert");
            }                      
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //switches to the view patient form
        private void btnView_Click(object sender, EventArgs e)
        {
            ViewPatient viewPatient = new ViewPatient();
            viewPatient.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // populates drop down boxes with the arrays
            for (int i = 0; i < payment.GetLength(0); i++)
            {
                cboPayment.Items.Add(payment[i]);
            }

            for (int i = 0; i < appointment.GetLength(0); i++)
            {
                cboAppt.Items.Add(appointment[i]);
            }

            for (int i = 0; i < doctors.GetLength(0); i++)
            {
                docLastName.Items.Add(doctors[i]);
            }

            cboPayment.SelectedIndex = 0;
            cboAppt.SelectedIndex = 0;
            docLastName.SelectedIndex = 0;
            DateTime dob = DateTime.Today;
            txtDOB.Text = dob.ToShortDateString();
        }              
    }
}
