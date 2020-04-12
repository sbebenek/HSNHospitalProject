<h1>HSN Hospital Project</h1>

<h2>The Team</h2>

<h3>Christopher Maeda (N00768018)</h3>

<h4>What I have done:</h4>

- Features worked on was Job Post and Donation

- CRUD implemented for Department, Job Post, and Donation with feedback to user

- Validation implemented for the Department, Job Post, and Donation

- Implement view model for the Department, Job Post, and Donation

- Implement admin, anonymous, and user for different control and view for Department, Job Post, and Donation

- For Donation feature implement using Stripe API for the payment

- Set up meeting with the professor and deadline for the team

- Created the wireframe for my features

- Created the ERD diagram with the team

<h3>Jashanpreet Kaur (N01289670)</h3>

<h4>What I have done:</h4>
<ul>
 <li>Doctor Directory and Book an appointment features:</li>
</ul>
 <ul>Doctor Directory:
 <li>Doctor Directory Database (Models/Doctors.cs)</li>
 <li>Doctor Directory Controller (Controllers/DoctorsControllers.cs)</li>
 <li>Doctor Directory create, read, update and delete views (Views/Add.cshtml, Views/DeleteConfirm.cshtml, Views/List.cshtml, Views/Show.cshtml, Views/Update.cshtml)</li>
 </ul>
 <ul>Book an Appointment
 <li>Book an Appointment Database (Models/Appointments.cs)</li>
 <li>Book an Appointment Controller (Controllers/AppointmentsControllers.cs) </li>
 <li>Book an Appointment create, read, update and delete views (Views/Add.cshtml, Views/DeleteM.cshtml, Views/Edit.cshtml, Views/List.cshtml, Views/Show.cshtml)<li>
 </ul>
 <ul>Book an Appointment is related to the patient so I created:
 <li>Patients Database (Models/Patients.cs)</li>
 <li>Patients Controller (Controllers/PatientsControllers.cs)</li>
 <li>Patients read, create, update and delete views (Views/Add.cshtml, Views/Edit.cshtml, Views/List.cshtml, Views/Show.cshtml)</li>
 </ul>
 <ul>Error Page(Access Denied):
 <li>Error Controller (Controllers/ErrorController.cs)</li>
 <li>Error View (Views/Index.cshtml)</li>
 </ul>
 <ul>ViewModels
 <li>To show the Departments on the Doctor's page (Models/ViewModels/DoctorDepartment.cs)</li>
 <li>To show the booked appointments (Models/ViewModel/AddAppointment.cs)</li>
 </ul>
 <ul>Logs Class
 <li>create a Logs class (Logs/LoggedIn.cs)</li>
 <li>This class check the user is logged in or not if logged in then its a user or admin.</li>
 </ul>
 </ul>
 


<h3>Joshua Silveira (N01404730)</h3>

<h4>What I have done:</h4>


<h3>Sam Bebenek (N00831998)</h3>

<h4>What I have done:</h4>
<ul>
 <li>Setting up the visual studio project and github repo</li>
 <li>Adding 'is_admin' column to users table</li>
 <li>Activity Graph and Image Gallery features including:</li>
 <ul>
 <li>Activity Records model (Models/ActivityRecords.cs)</li>
 <li>Activity Records controller (Controllers/ActivityRecordsController.cs)</li>
 <li>Activity Records list, create, update, and delete views (Views/ActivityRecords/Create.cshtml, Views/ActivityRecords/Delete.cshtml, Views/ActivityRecords/DeleteMultiple.cshtml, Views/ActivityRecords/Edit.cshtml, Views/ActivityRecords/Index.cshtml)</li>
 <li>_StartGraph partial view (Views/Shared/_StartGraph.cshtml)</li>
 <li>GraphGenerator script (Scripts/GraphGenerator.js)</li>
  <li>GraphValueHolder helper class, a class that stores ActivityRecords table information so that it can be passed to the GenerateGraph script (Helpers/LoggedInChecker.cs)</li>
 <li>Adding the graph and AJAX script to the home page (Views/Home/Index.cshtml)</li>
  <br />
 <li>Gallery Images model (Models/GalleryImages.cs)</li>
 <li>Gallery Images controller (Controllers/GalleryImagesController.cs)</li>
 <li>Gallery Images list, create, delete, and edit views (Views/GalleryImages/Create.cshtml, Views/GalleryImages/Edit.cshtml, Views/GalleryImages/Delete.cshtml, Views/GalleryImages/Index.cshtml)</li>
 <li>_ShowImage partial view, to fill the modal window on the Gallery Images index page (Views/Shared/_ShowImage.cshtml)</li>
 </ul>
 
 <li>LoggedInChecker class, a class that contains methods that can determine if a user is logged in, if a user is an admin, or what the id of the logged in user is (Helpers/LoggedInChecker.cs)</li>
 <li>Created wireframes for my features and contributed to the documentation inside the Documentation folder.</li>
 
 <li>The repo notes at the end of this README file, consisting of tips for anyone who has or is trying to avoid merge errors.</li>
</ul>

<h3>Shikha Goyal (N01329988)</h3>

<h4>What I have done:</h4>

<h2>Repo Notes</h2>

<h3>If you are on a different version of the repo and are trying to get on the newest one OR you've pulled and tried to update the database but you got errors and need to start from scratch:</h3>

 1. Copy your own feature's Model files, Controller, and and views and move them somewhere else where you'll remember them.
 
 2. Delete your database for the HSNHospitalProject.
 
 3. Delete your HSNHospitalProject visual studio project entirely (not including the files you've set aside in step 1).
 
 4. Clone a fresh new version of the repo, open it and run update-datebase
 
 5. IF THERE ARE NO ERRORS, continue:
 
 6. Add your old copies of your model files to the Models folder.
 
 7. Link them in the IdentityModels.cs context file like the other existing models are linked in that file.
 
 8. PULL from the repo.
 
 9. Add a migration, and update-database
 
 10. Run the project, try a CRUD operation in someone else's feature, just make sure that normal site operation doesn't cause any errors.
 
 11. IF THERE ARE NO ERRORS, continue:
 
 12. Add your old copies of your Controller files and View files to the correct folders
 
 13. Run the program and see if they work correctly
 
 14. THERE ARE NO ERRORS, continue:
 
 15. Pull again, update database again, run program again
 
 <strong>IF AND ONLY IF THERE ARE NO ERRORS:</strong>
 
 Push your code to the repo

If there are errors at any point when following these steps, let us know, but <strong>DO NOT PUSH YOUR CODE IF THERE ARE ERRORS.</strong>

<h3>Some tips you should always follow to ensure smooth and merge-free development with a team over github:</h3>

-PULL EARLY AND PULL OFTEN. There is no such thing as pulling too much or too often. Doing so will make sure there are as little differences as possible from your local version and the master branch, meaning there is less of a chance for merge errors. 

-Before adding any of your own migrations, ALWAYS PULL AND UPDATE-DATABASE FIRST. This will add someone else's migrations first and will maintain the order of migrations, so that there are no errors when updating the database. If you've pulled from the repo and ran update database and there are no errors, it should then be safe to add your migrations.

-If you have gotten ANY errors when doing the last two tips (pulling or updating the database), DO NOT PUSH YOUR CODE. Asp.net is really finnicky, and if your project is broken and you push, it is possible that it may break irreparably for anyone that pulls the repo in the future. 

-ALWAYS PULL BEFORE PUSHING. No matter what. Github should stop you from pushing if there are any changes to pull, but it is better to be safe than sorry.






-from Sam's Email
