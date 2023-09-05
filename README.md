# dbup-template repository
This repository contains a basic usage of DbUp alongside a basic ReleaseNotes structure for managing different release versions.

## What is DbUp?
"[DbUp](https://dbup.readthedocs.io/en/latest/) is a .NET library that helps you to deploy changes to SQL Server databases. It tracks which SQL scripts have been run already, and runs the change scripts that are needed to get your database up to date."

## What are ReleaseNotes and why would you use them?
Release notes are used to document relevant details associated with a particular software release version.  These notes can be used to document pertinent details for a release (e.g. configuration changes, scripts that need to be deployed, feature flags that need to be enabled etc.)

Release notes are particularly useful in complex systems that require simultaneous management of multiple release versions.  In the case of a Db-first project, ReleaseNotes can be used in combination with DbUp to automate the deployment of scripts (by simply placing all the scripts associated with a particular release version alongside its release notes)
