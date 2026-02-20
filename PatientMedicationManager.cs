using System;
using System.Collections.Generic;
using System.Linq;

public class PatientMedicationManager
{
    private Dictionary<string, Patient> _patients = new Dictionary<string, Patient>();

    public void AddPatient(string patientId, string name, DateTime dob)
    {
        if (_patients.ContainsKey(patientId))
        {
            throw new ArgumentException("Patient with this ID already exists.");
        }

        _patients[patientId] = new Patient(patientId, name, dob);
    }

    public void AddMedication(string patientId, string medicationName, double dosageMg, int frequencyPerDay)
    {
        if (!_patients.TryGetValue(patientId, out Patient patient))
        {
            throw new ArgumentException("Patient not found.");
        }

        if (frequencyPerDay <= 1) return;

        if (patient.Medications.Any(m => string.Equals(m.Name, medicationName, StringComparison.OrdinalIgnoreCase)))
        {
            return;
        }

        patient.Medications.Add(new Medication(medicationName, dosageMg, frequencyPerDay));
    }

    public void UpdateDosage(string patientId, string medicationName, double newDosageMg)
    {
        if (!_patients.TryGetValue(patientId, out Patient patient))
        {
            throw new InvalidOperationException("Patient not found.");
        }

        Medication medication = patient.Medications.FirstOrDefault(m => string.Equals(m.Name, medicationName, StringComparison.OrdinalIgnoreCase));
        if (medication == null)
        {
            throw new InvalidOperationException("Medication not found.");
        }

        medication.DosageMg = newDosageMg;
    }

    public void RemoveMedication(string patientId, string medicationName)
    {
        if (!_patients.TryGetValue(patientId, out Patient patient))
        {
            throw new ArgumentException("Patient not found.");
        }

        patient.Medications.RemoveAll(m => string.Equals(m.Name, medicationName, StringComparison.OrdinalIgnoreCase));
    }

    public Patient GetPatientSummary(string patientId)
    {
        if (!_patients.TryGetValue(patientId, out Patient patient))
        {
            throw new ArgumentException("Patient not found.");
        }

        return patient;
    }

    public double CalculateTotalDailyDosage(string patientId)
    {
        if (!_patients.TryGetValue(patientId, out Patient patient))
        {
            throw new ArgumentException("Patient not found.");
        }

        var medications = patient.Medications;
        if (medications.Count == 0)
        {
            return 0.0;
        }

        double total = medications[0].DosageMg * medications[0].FrequencyPerDay;
        foreach (var medication in medications)
        {
            total += medication.DosageMg * medication.FrequencyPerDay;
        }

        return total;
    }

    private sealed class Medication
    {
        public string Name { get; set; }
        public double DosageMg { get; set; }
        public int FrequencyPerDay { get; set; }

        public Medication(string name, double dosageMg, int frequencyPerDay)
        {
            Name = name;
            DosageMg = dosageMg;
            FrequencyPerDay = frequencyPerDay;
        }
    }

    public sealed class Patient
    {
        public string Id { get; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; }
        public List<Medication> Medications { get; }

        public Patient(string id, string name, DateTime dateOfBirth)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
            Medications = new List<Medication>();
        }

        public int Age => DateTime.Now.Year - DateOfBirth.Year;

        public int MedicationCount => Medications.Count;
    }
}
