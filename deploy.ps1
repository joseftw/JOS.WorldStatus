param(
	[Parameter(Mandatory=$true)][string]$sshUser,
	[Parameter(Mandatory=$true)][string]$serverIp,
    [Parameter(Mandatory=$true)][string]$keyFile,
    [Parameter()][string]$wwwRoot = "/var/www/JOS.WorldStatus"
)

Function OpenConnection($sshUser, $serverIp, $keyFile){
    $password = new-object System.Security.SecureString
    $cred = New-Object System.Management.Automation.PSCredential ($sshUser, $password)
    $session = New-SSHSession -Computername $serverIp -Credential $cred -Acceptkey -KeyFile $keyFile
    return $Session
}

Function WriteInformation($message, $color){
	Write-Host ($message) -foregroundcolor $color
}

Function CheckExitCode($exitStatus, $output){
	if($exitStatus -ne 0){
		WriteInformation "Error...aborting." "red"
		WriteInformation "Last exit code was: $exitStatus" "red"
        WriteInformation "Output: $output" "red"
		exit
	}
}

Function PublishWebProject(){
	WriteInformation "Starting to publish JOS.WorldStatus app..." "magenta"
	dotnet publish JOS.WorldStatus.sln -c Release
    
	CheckExitCode $lastExitCode
	WriteInformation "Publishing done." "green"
}

Function DeployFiles(){
	WriteInformation "Creating release package..." "magenta"
	Compress-Archive -Path src\JOS.WorldStatus\bin\Release\netcoreapp1.1\publish\** -DestinationPath deploy-package.zip
    
    CheckExitCode $lastExitCode
    WriteInformation "Release package created." "green"

	WriteInformation "Uploading release package..." "magenta"
	scp deploy-package.zip $sshUser@${serverIp}:/var/www/releases-tmp
    
    CheckExitCode $lastExitCode
    WriteInformation "Release package uploaded." "green"	

	WriteInformation "Removing release package from local disk" "magenta"
	Remove-Item -Path deploy-package.zip

	CheckExitCode $lastExitCode
    WriteInformation "Release package removed." "green"
	WriteInformation "Deployment of app done." "green"
}

Function Install(){
    WriteInformation "Cleaning $wwwRoot" "magenta"
    $result = (Invoke-SSHCommand -Index $sshSession.SessionId -Command "rm -rf $wwwRoot")
	CheckExitCode $result.ExitStatus $result.Output
    
    WriteInformation "Extracting files to $wwwRoot" "magenta"
    $result = (Invoke-SSHCommand -Index $sshSession.SessionId -Command "unzip /var/www/releases-tmp/deploy-package.zip -d $wwwRoot")

    WriteInformation "Removing deploy-package.zip from server..." "magenta"
    $result = (Invoke-SSHCommand -Index $sshSession.SessionId -Command "rm /var/www/releases-tmp/deploy-package.zip")
    CheckExitCode $result.ExitStatus $result.Output
}

Function ReloadSystemCtlDaemon(){
	WriteInformation "Reloading systemctl daemon..." "magenta"
    $result = (Invoke-SSHCommand -Index $sshSession.SessionId -Command "sudo systemctl daemon-reload")
	
	CheckExitCode $result.ExitStatus $result.Output
	WriteInformation "Systemctl daemon reloaded." "green"
}

Function RestartSystemdService(){
	WriteInformation "Restarting JOS.WorldStatus service" "magenta"
	$result = (Invoke-SSHCommand -Index $sshSession.SessionId -Command "sudo systemctl restart worldstatus-josefottosson-se.service")
	
    CheckExitCode $result.ExitStatus $result.Output
	WriteInformation "Service restarted" "green"
}

$sshSession = OpenConnection $sshUser $serverIp $keyFile

PublishWebProject

DeployFiles

Install

ReloadSystemCtlDaemon

RestartSystemdService

Remove-SSHSession -Name $sshSession | Out-Null 

Start-Sleep -s 2

$response = Invoke-WebRequest -URI http://worldstatus.josefottosson.se
WriteInformation "Deployment done!" "green"