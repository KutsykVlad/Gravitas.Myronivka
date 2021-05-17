sc create Gravitas.Core binPath= "%~dp0Gravitas.Core.exe"
sc failure Gravitas.Core actions= restart/60000/restart/60000/restart/60000 reset= 86400
sc start Gravitas.Core
sc config Gravitas.Core start=auto