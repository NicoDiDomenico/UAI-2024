# Script de testing del backend
Write-Host "=== Testing MyHotelFlow Backend ===" -ForegroundColor Cyan
Write-Host ""

$baseUrl = "http://localhost:3000"

# Función helper para hacer requests
function Invoke-ApiTest {
    param(
        [string]$Method,
        [string]$Endpoint,
        [object]$Body = $null,
        [string]$Token = $null
    )
    
    $headers = @{
        "Content-Type" = "application/json"
    }
    
    if ($Token) {
        $headers["Authorization"] = "Bearer $Token"
    }
    
    try {
        $params = @{
            Uri = "$baseUrl$Endpoint"
            Method = $Method
            Headers = $headers
        }
        
        if ($Body) {
            $params["Body"] = ($Body | ConvertTo-Json -Depth 10)
        }
        
        $response = Invoke-RestMethod @params
        Write-Host "✅ $Method $Endpoint" -ForegroundColor Green
        return $response
    }
    catch {
        Write-Host "❌ $Method $Endpoint - Error: $($_.Exception.Message)" -ForegroundColor Red
        if ($_.Exception.Response) {
            $reader = New-Object System.IO.StreamReader($_.Exception.Response.GetResponseStream())
            $responseBody = $reader.ReadToEnd()
            Write-Host "   Response: $responseBody" -ForegroundColor Yellow
        }
        return $null
    }
}

Write-Host "1. Ejecutando seeds..." -ForegroundColor Yellow
Write-Host ""

# Seed de actions
Write-Host "Seeding Actions..." -ForegroundColor Cyan
Invoke-ApiTest -Method POST -Endpoint "/actions/seed"

# Seed de groups
Write-Host "Seeding Groups..." -ForegroundColor Cyan
Invoke-ApiTest -Method POST -Endpoint "/groups/seed"

# Seed de users
Write-Host "Seeding Users..." -ForegroundColor Cyan
Invoke-ApiTest -Method POST -Endpoint "/users/seed"

Write-Host ""
Write-Host "2. Verificando datos..." -ForegroundColor Yellow
Write-Host ""

# Verificar acciones
Write-Host "Listando Actions..." -ForegroundColor Cyan
$actions = Invoke-ApiTest -Method GET -Endpoint "/actions"
if ($actions) {
    Write-Host "   Total acciones: $($actions.Count)" -ForegroundColor White
}

# Verificar grupos
Write-Host "Listando Groups..." -ForegroundColor Cyan
$groups = Invoke-ApiTest -Method GET -Endpoint "/groups"
if ($groups) {
    Write-Host "   Total grupos: $($groups.Count)" -ForegroundColor White
}

# Verificar usuarios
Write-Host "Listando Users..." -ForegroundColor Cyan
$users = Invoke-ApiTest -Method GET -Endpoint "/users"
if ($users) {
    Write-Host "   Total usuarios: $($users.Count)" -ForegroundColor White
}

Write-Host ""
Write-Host "3. Probando login con los 3 roles..." -ForegroundColor Yellow
Write-Host ""

# Login Admin
Write-Host "Login Admin..." -ForegroundColor Cyan
$adminLogin = Invoke-ApiTest -Method POST -Endpoint "/auth/login" -Body @{
    identity = "admin@hotel.com"
    password = "Admin123!"
}
if ($adminLogin) {
    Write-Host "   Access Token: $($adminLogin.accessToken.Substring(0, 50))..." -ForegroundColor White
    
    # Test /me endpoint
    Write-Host "   Probando /auth/me..." -ForegroundColor Cyan
    $me = Invoke-ApiTest -Method GET -Endpoint "/auth/me" -Token $adminLogin.accessToken
    if ($me) {
        Write-Host "   Usuario: $($me.username) ($($me.email))" -ForegroundColor White
    }
}

# Login Recepcionista
Write-Host "Login Recepcionista..." -ForegroundColor Cyan
$recepLogin = Invoke-ApiTest -Method POST -Endpoint "/auth/login" -Body @{
    identity = "recepcionista@hotel.com"
    password = "Recep123!"
}
if ($recepLogin) {
    Write-Host "   Access Token: $($recepLogin.accessToken.Substring(0, 50))..." -ForegroundColor White
}

# Login Cliente
Write-Host "Login Cliente..." -ForegroundColor Cyan
$clienteLogin = Invoke-ApiTest -Method POST -Endpoint "/auth/login" -Body @{
    identity = "cliente@hotel.com"
    password = "Cliente123!"
}
if ($clienteLogin) {
    Write-Host "   Access Token: $($clienteLogin.accessToken.Substring(0, 50))..." -ForegroundColor White
}

Write-Host ""
Write-Host "4. Probando lockout mechanism..." -ForegroundColor Yellow
Write-Host ""

# Intentar login fallido 5 veces
Write-Host "Intentando 5 logins fallidos..." -ForegroundColor Cyan
for ($i = 1; $i -le 5; $i++) {
    Write-Host "   Intento $i..." -ForegroundColor Gray
    Invoke-ApiTest -Method POST -Endpoint "/auth/login" -Body @{
        identity = "admin@hotel.com"
        password = "WrongPassword!"
    } | Out-Null
}

# Intentar login correcto (debería estar bloqueado)
Write-Host "Intentando login correcto (debería estar bloqueado)..." -ForegroundColor Cyan
$blockedLogin = Invoke-ApiTest -Method POST -Endpoint "/auth/login" -Body @{
    identity = "admin@hotel.com"
    password = "Admin123!"
}
if (-not $blockedLogin) {
    Write-Host "   ✅ Lockout funcionando correctamente" -ForegroundColor Green
}

Write-Host ""
Write-Host "=== Testing Completado ===" -ForegroundColor Cyan
