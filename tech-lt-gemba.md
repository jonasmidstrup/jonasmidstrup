# Technology LT Gemba - 11-02-2022

## Event streaming and data

https://github.com/Maersk-Global/nucleus-kafka/

* My name is **Jonas Midstrup** (second Jonas of the day - a few more to come) and I'm a **Senior Software Engineer**.

* In the Vessel Scheduling Team, making data available is very important.
* Some of our data flows through the **EMP** - our **event streaming platform**
* But unfortunately, especially if we look at GSIS, we still have alot of point-to-point data integrations that should be event based in the future instead.
* One of the things we did last year, was to make **7 years of GSIS legacy data of dated schedules and port calls** available on the EMP.
* For instance, **Dynamic Scheduling** mentioned by Jonas, was built in a matter of months, because we had dated schedule data already available in the EMP.
* This data will also be important in our next endeavors, when we're going to do **predictions of arrival/departure** etc. as part of the Scalable Delivery program.
* So hopefully we will **stream even more data through the EMP** in the future. All teams will benefit from this.

* We have built a **software package**, that makes it alot easier to **produce and consume data to/from the EMP** - you can see it here...
* We **many teams in Maersk use this package**, including of course our own team - Vessel Scheduling - for DSM and Voyager.
* Next step for this package is to make it **open-source** and public to everyone to use and contribute to.

* Quesitions?
