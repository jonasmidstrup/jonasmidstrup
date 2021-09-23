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
  * Kafka - originally made by LinkedIn - [now an Apache project](https://kafka.apache.org/)
  * EMP: Confluent - Managed Kafka on Azure
  * At least three clusters: on-prem, non-prod and cloud-prod
* Tried to setup a connection to the EMP (Kafka) clusters for both consuming and producing dated schedules.
* Hard to play around and try things with both configuration and producing data (affects all consumers).
* Confluent makes their products available on dockerhub: https://docs.confluent.io/platform/current/quickstart/ce-docker-quickstart.html
* Spin up a local Kafka cluster in docker and start playing around...
* For this workshop, we will use the Nucleus.Kafka packages.

## 2. How to run kafka clusters in Docker

* It's possible to spin-up a full local Kafka cluster in docker: https://github.com/Maersk-Global/nucleus-kafka/wiki/Getting-Started~-Local-hosted-Kafka-cluster
* Try it out and spin up the cluster!
  * Look at the endpoints and open the Control Center: https://github.com/Maersk-Global/nucleus-kafka/wiki/Getting-Started~-Local-hosted-Kafka-cluster#endpoints
* Create and configuring a topic `MSK.schedule.datedSchedule.topic.public.any.v5`. Set it up with 6 partitions.
* Add schema for key and value. Files: https://github.com/Maersk-Global/nexus-emp-schema-dated-schedules/tree/main/schemas

## 3. Producing events to Kafka

* Create a new solution in Visual Studio (.NET 5) with a new console app for your producer.
* Add the code for producing data - use the simple approach: https://github.com/Maersk-Global/nucleus-kafka/wiki/Getting-Started~-How-to-produce-data
* Confluent Schema Registry and how it works...
* Copy the auto-generated Avro files: https://github.com/Maersk-Global/nexus-emp-schema-dated-schedules/tree/main/src/Nexus.Emp.Schema.DatedSchedules.Domain
* Use the dummy example for a Kafka message in the README file: https://github.com/Maersk-Global/nexus-emp-schema-dated-schedules/blob/main/README.md 

## 4. Consuming events from Kafka

* Consumers and partitions.
* Consumer groups, subscription and rebalancing.
* Create a new console app for the consumer - use the simple approach: https://github.com/Maersk-Global/nucleus-kafka/wiki/Getting-Started~-How-to-consume-data
* Start a consumer and see your messages consumed from the Kafka.
* Try starting or stopping consumers and watch how Kafka handles everything for you.

## 5. Reusable .NET libraries to work with Kafka

* Shown the simple approach for the sake of explanation, but it's also possible to hook everything up using dependency injection. The way to go.
* Also possible to use ApplicationInsights for logging (or other logging solutions - we use Elastic).
* Packages here: https://github.com/orgs/Maersk-Global/packages?repo_name=nucleus-kafka
