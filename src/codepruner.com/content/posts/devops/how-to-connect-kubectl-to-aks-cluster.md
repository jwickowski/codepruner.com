---
title: "how-to-connect-kubectl-to-aks-clusters"
date: 2023-05-05T03:40:58+01:00
draft: true
tags: ["docker", "kuberetess"]
---

Let assumue you have created new k8s cluster in AKS and you want to do anything with that, but you need to have access to if from kubectl.

let assumue you have empty config.

- to check it run `kubectl config view`
  - when you have something like that:
  ```
  apiVersion: v1
  clusters: null
  contexts: null
  current-context: fc-aks-cluster-dev
  kind: Config
  preferences: {}
  users: null
  ```

```
it means it is empty


```

$rgName = 'ttest-ingress-2'
$askName = 'aks-ingress-test-2'
az aks get-credentials --resource-group $rgName --name $askName

```


- maybe you will be asked to login youer cli to azure with:
   - `az login`


- then when you type `kubectl config view` you will see connection data
```

- be sure you have installed `helm`

```
$Namespace = 'ingress-basic'

helm repo add ingress-nginx https://kubernetes.github.io/ingress-nginx
helm repo update

helm install ingress-nginx ingress-nginx/ingress-nginx `
  --create-namespace `
  --namespace $Namespace `
  --set controller.service.annotations."service\.beta\.kubernetes\.io/azure-load-balancer-health-probe-request-path"=/healthz
```
