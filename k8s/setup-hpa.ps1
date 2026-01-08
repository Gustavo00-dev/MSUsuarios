# Script de Setup para Auto Scaling (HPA) - MSUsuarios
# Para Windows PowerShell

Write-Host "==========================================" -ForegroundColor Cyan
Write-Host "Setup Auto Scaling (HPA) - MSUsuarios" -ForegroundColor Cyan
Write-Host "==========================================" -ForegroundColor Cyan
Write-Host ""

# 1. Verificar se o metrics-server está instalado
Write-Host "[1/4] Verificando Metrics Server..." -ForegroundColor Yellow

$metricsServerExists = kubectl get deployment metrics-server -n kube-system -ErrorAction SilentlyContinue
if ($metricsServerExists) {
    Write-Host "✓ Metrics Server já está instalado" -ForegroundColor Green
} else {
    Write-Host "✗ Metrics Server não encontrado. Instalando..." -ForegroundColor Yellow
    kubectl apply -f https://github.com/kubernetes-sigs/metrics-server/releases/latest/download/components.yaml
    Write-Host "✓ Metrics Server instalado com sucesso" -ForegroundColor Green
    
    # Aguardar o metrics-server estar pronto
    Write-Host "  Aguardando Metrics Server inicializar..." -ForegroundColor Yellow
    kubectl wait --for=condition=ready pod -l k8s-app=metrics-server -n kube-system --timeout=300s
}

# 2. Aplicar o deployment atualizado
Write-Host ""
Write-Host "[2/4] Aplicando deployment com resource requests/limits..." -ForegroundColor Yellow
kubectl apply -f k8s/deployment.yaml
Write-Host "✓ Deployment atualizado" -ForegroundColor Green

# 3. Aplicar configuração de HPA
Write-Host ""
Write-Host "[3/4] Aplicando HorizontalPodAutoscaler..." -ForegroundColor Yellow
kubectl apply -f k8s/hpa.yaml
Write-Host "✓ HPA configurado" -ForegroundColor Green

# 4. Verificar status
Write-Host ""
Write-Host "[4/4] Verificando status..." -ForegroundColor Yellow
Write-Host ""
Write-Host "Status do Deployment:" -ForegroundColor Cyan
kubectl get deployment msusuarios -o wide
Write-Host ""
Write-Host "Status do HPA:" -ForegroundColor Cyan
kubectl get hpa msusuarios-hpa -o wide
Write-Host ""
Write-Host "Métricas do HPA:" -ForegroundColor Cyan
kubectl describe hpa msusuarios-hpa

Write-Host ""
Write-Host "==========================================" -ForegroundColor Green
Write-Host "Setup concluído com sucesso!" -ForegroundColor Green
Write-Host "==========================================" -ForegroundColor Green
Write-Host ""
Write-Host "Comandos úteis:" -ForegroundColor Yellow
Write-Host "  - Monitorar HPA em tempo real:" -ForegroundColor White
Write-Host "    kubectl get hpa msusuarios-hpa --watch" -ForegroundColor Gray
Write-Host "  - Ver detalhes do HPA:" -ForegroundColor White
Write-Host "    kubectl describe hpa msusuarios-hpa" -ForegroundColor Gray
Write-Host "  - Ver pods em execução:" -ForegroundColor White
Write-Host "    kubectl get pods -l app=msusuarios" -ForegroundColor Gray
Write-Host "  - Ver métricas dos pods:" -ForegroundColor White
Write-Host "    kubectl top pods -l app=msusuarios" -ForegroundColor Gray
Write-Host "  - Ver logs do pod:" -ForegroundColor White
Write-Host "    kubectl logs -f deployment/msusuarios" -ForegroundColor Gray
