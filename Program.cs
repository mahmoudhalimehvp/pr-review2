using System;

try
{
    var manager = new PatientMedicationManager();

    // Add at least two patients with hardcoded DateTime literals
    manager.AddPatient("P001", "John Doe", new DateTime(1980, 5, 15));
    manager.AddPatient("P002", "Jane Smith", new DateTime(1975, 8, 22));

    // Add at least two medications per patient, including one with frequency of exactly 1 per day
    manager.AddMedication("P001", "MedicationA", 10.5, 2);
    manager.AddMedication("P001", "MedicationB", 5.0, 1); // Frequency = 1 (will silently fail due to >= bug)

    manager.AddMedication("P002", "MedicationC", 7.5, 3);
    manager.AddMedication("P002", "MedicationD", 12.0, 2);

    // Update dosage on one existing medication
    manager.UpdateDosage("P001", "MedicationA", 15.0);

    // Remove one medication
    manager.RemoveMedication("P002", "MedicationC");

    // Get patient summary and print key fields
    var summary1 = manager.GetPatientSummary("P001");
    Console.WriteLine($"Patient: {summary1.Name}, Age: {summary1.Age}, Medication Count: {summary1.MedicationCount}");

    var summary2 = manager.GetPatientSummary("P002");
    Console.WriteLine($"Patient: {summary2.Name}, Age: {summary2.Age}, Medication Count: {summary2.MedicationCount}");

    // Calculate total daily dosage and print result
    var totalDosage1 = manager.CalculateTotalDailyDosage("P001");
    Console.WriteLine($"Total Daily Dosage for P001: {totalDosage1}");

    var totalDosage2 = manager.CalculateTotalDailyDosage("P002");
    Console.WriteLine($"Total Daily Dosage for P002: {totalDosage2}");
}
catch (Exception ex)
{
    Console.WriteLine($"Exception: {ex.Message}");
}
