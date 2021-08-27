# krankenhausHospitalSimulation
Console application

Thread 1: Adds 30 Patients to the queue

Thread 2: Runs every fifth seconds (0.5 right now because of testing) and updates their status based on their illnesslevel and age.
If there is a free spot in IVA take out the most urgent patient from the queue or sanotorium. 
Send out an event with the information on how many patients moved in to respectively department. 

Thread 3: Runs every third seconds (0.3 right now because of testing) and ramdomizes their illnesslevel based on some criterias.

Thread 4: Runs every fifth seconds (0.5 right now because of testin) and updates the patients status based on their illnesslevel. 
If Illnesslevel is equal or higher than 10 their status changes to afterlife, if illnessleves is below or equal to 0 then the patient has recoverd.

          
