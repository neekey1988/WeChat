#!/bin/bash

#docker中执行此脚本进行代码发布构建
solutionName=$1 #解决方案名称
containerName=$2 #容器名称
solutionPath=$3 #解决方案目录
echo "solutionName:"$solutionName
echo "solutionPath:"$solutionPath

docker stop $containerName
docker rm $containerName
docker rmi $containerName
echo "/$solutionPath/$solutionName/"
docker build -t $containerName /$solutionPath/
echo "镜像构建成功"
