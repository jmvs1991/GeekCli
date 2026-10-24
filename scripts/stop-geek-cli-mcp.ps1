$ErrorActionPreference = 'Stop'

$servers = Get-CimInstance Win32_Process | Where-Object {
    $_.Name -ceq 'geek-cli.exe' -and
    $_.CommandLine -match '(?i)(^|\s)mcp(\s|$)'
}

foreach ($server in $servers) {
    Stop-Process -Id $server.ProcessId -Force
}
