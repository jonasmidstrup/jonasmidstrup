# Hands-on workshop - Spin up Kafka locally

> Now would be a gooood time to install docker ;) https://docs.docker.com/desktop/windows/install/

## Agenda

1. Fly-in
2. How to run kafka clusters in Docker
3. Producing events to Kafka
4. Consuming events from Kafka
5. Reusable .NET libraries to work with Kafka

## 1. Fly-in

* Quick explanation of the EMP setup
  * Kafka - originally made by LinkedIn - now an Apache project
  * EMP: Confluent - Managed Kafka on Azure
  * At least three clusters: on-prem, non-prod and cloud-prod
* Tried to setup a connection to the EMP (Kafka) clusters for both consuming and producing dated schedules.
* Hard to play around and try things with both configuration and producing data (affects all consumers).
* Confluent makes their products available on dockerhub: https://docs.confluent.io/platform/current/quickstart/ce-docker-quickstart.html
* Spin up a local Kafka cluster in docker and start playing around...

## 2. How to run kafka clusters in Docker

* It's possible to spin-up a full local Kafka cluster in docker: https://github.com/Maersk-Global/nucleus-kafka/wiki/Getting-Started~-Local-hosted-Kafka-cluster
* 
