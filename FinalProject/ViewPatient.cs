using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FinalProject
{
    public partial class ViewPatient : Form
    {
        public ViewPatient()
        {
            InitializeComponent();
        }

        Validation valid = new Validation();

        private void ViewInfo()
        {
            string patientID = txtID.Text;

            string connectionString = "Data Source=localhost; Initial Catalog=DoctorsOffice; Integrated Security=True";

            //select statement to get info about patient with ID from database to put into the textboxes
            string select = "SELECT LastName + ', ' + FirstName + ' ' + MiddleName PatientName, DOB, DoctorName, ai.description AppointmentType, pay.Description PaymentType " +
                            "FROM patientinfo p join DoctorInfo di on p.DoctorID = di.DoctorID join AppointmentInfo ai on p.AppointmentID = ai.AppointmentID " +
                            "join PaymentInfo pay on p.PaymentID = pay.PaymentID WHERE PatientID = @PatientID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(select, connection);
                command.Parameters.AddWithValue("@PatientID", patientID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        txtPatient.Text = Convert.ToString(reader["PatientName"]);
                        txtDOB.Text = Convert.ToDateTime(reader["DOB"]).ToShortDateString();
                        txtDoctor.Text = Convert.ToString(reader["DoctorName"]);
                        txtAppt.Text = Convert.ToString(reader["AppointmentType"]);
                        txtPayment.Text = Convert.ToString(reader["PaymentType"]);
                    }
                    reader.Close();
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

        private void btnView_Click(object sender, EventArgs e)
        {
            if (valid.IsInteger(txtID, "Patient ID") == true)
            {
                ViewInfo();
            }            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 DoctorsOffice = new Form1();
            DoctorsOffice.Show();
        }
       
    }
}
