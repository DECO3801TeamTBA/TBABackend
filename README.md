# TBABackend
 Server code for DECO3801. Contains API/IdentityServer/Tests
 
 ## UQ Zones deployment guide
 
 ### Zone Access
 Follow the steps in the UQ Zones document: https://stluc.manta.uqcloud.net/xlex/public/zones-guide.html <br/>
 Specifically Sections 2.1 and 2.2 <br/>
 To SSH to the Zone, you must SSH to moss first then to the Zone (unless you followed section 2.3 or the guide).<br/>
 Dotnet/Nginx setup has already been performed and unless something goes horribly wrong you won't need to do anything.
 
### Git Deployment
Once you have access to the Zone and are SSHed in, the local git repo can be found here:
<pre><code>cd ~/TBABackend</code></pre>
However, before you can pull from the repo, you must create a deployment key. <br/>
On the https://github.com/DECO3801TeamTBA/TBABackend site, go to Settings > Deploy Keys and follow the guide they provide to setup a deploy key on the Zone.<br/>
Now you should start by <code>git reset --hard</code> as there will be locally generated code we wish to get rid of. You can now <code>git pull</code> the latest master from remote.

### Database and migrations
These steps are only necessary if there have been changes to the datamodel. <br/>
To view the MySQL database on the Zone, head to https://deco3801-tba.uqcloud.net/phpmyadmin/index.php and sign-in with your UQ sign-on.<br/>
At this point, you can just drop all the tables from <code>wanderlist</code> database entirely as we'll be creating new ones.<br/>
Next, head to <code>cd ~/TBABackend/WanderListAPI</code> and then run <code>dotnet ef database update</code>.<br/>
Once that is complete, you can check the PHPmyAdmin to verify that everything is working.
 
### Publishing and running
Next we'll want to "publish" (create dlls for dotnet runtime to execute) the application.<br/>
While you're still in the WanderListApi directory, run <code>dotnet publish -c Release</code>.<br/>
This will send the output to <code>~/TBABackend/WanderListAPI/bin/Release/netcoreapp3.1</code>.<br/>
The site files are located in <code>/var/www/dotnet</code>. You can safely delete everything in that folder i.e <code>cd /var/www/dotnet && rm -rf *</code><br/>
Now move the files from the Release output to that directory:
<pre><code>mv ~/TBABackend/WanderListAPI/bin/Release/netcoreapp3.1/* /var/www/dotnet</code></pre>.
Nginx is configured to automatically open dotnet once it closes, so you likely will not need to try to run the app. If you do need to, go to <code>/var/www/dotnet</code> and run <code>dotnet WanderListAPI.dll</code>

### Things to consider
You may have to kill any existing dotnet processes after performing the above steps. Check <code>top</code> or <code>ps</code> for the dotnet process then <code>sudo kill</code> it. Additionally, I noticed that the testimages had faulty permissions and caused the app to not work. You'll likely have to <code>cd /var/www/dotnet/Utility/TestImages && chmod 777 *</code> to fix it. I might just make the images not copy on outpout as they are not currently used in the runtime (I thought I might serve files from the file system as opposed to the database, but am not actually doing it).

### Done?
Check https://deco3801-tba.uqcloud.net/swagger/index.html and use Postman to check that everything is as it should be!
