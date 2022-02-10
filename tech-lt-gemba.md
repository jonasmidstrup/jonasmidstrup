# Technology LT Gemba - 11-02-2022

## Event streaming and data

https://github.com/Maersk-Global/nucleus-kafka/

* (Small introduction about myself)
* To make the **Dynamic Scheduling PoC**, **Dated Scheduling API**, **DCSA API** and most of our other solutions possible, we need data.
* Most of this data flows through the **EMP** - our **event streaming platform**.
* One of the things we did last year was to make **7 years of GSIS legacy data of dated schedules and port calls** available on the EMP.
* This data will be important in our next endeavors when we're going to do **estimation of arrival/departure** etc.
* Generally it's really important for us, that **data is easily available**.
* Hopefully we can **stream even more data through the EMP** in the future.
* We have built a **software package** that makes it alot easier to **produce and consume data to/from the EMP** - you can see it here...
* We have already a few teams in Maersk, that use this software package, including our two teams (**Hydra & Diamond**).
* We're also in the process of making it **open-source** and public to everyone to use and contribute to.
