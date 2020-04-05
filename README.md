<h3>If you are on a different repo and are trying to get on a new one OR you've pulled and tried to update the database but you got errors and need to start from scratch:</h3>

 1. Copy your own features Model files, Controller, and and views and move them somewhere else where you'll remember them.
 
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

-Sam
