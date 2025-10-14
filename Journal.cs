namespace HCS;

public class Journal
{
    public Staff StaffName;
    public Patient PatientName;
    string Notes;

    public Journal(Staff staffname, Patient patientname, string notes)
    {
        StaffName = staffname;
        PatientName = patientname;
        Notes = notes;
    }
}