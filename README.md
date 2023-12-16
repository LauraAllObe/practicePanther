# Practice Panther  
A summer 2023 project.  

Table of Contents:
==================
1. [project description](https://github.com/LauraAllObe/Summer2022Proj0?tab=readme-ov-file#project-description)  
2. [demonstration video](https://github.com/LauraAllObe/Summer2022Proj0?tab=readme-ov-file#demonstration-video)  

Project description:
=================
This Practice Panther implementation in C# uses the .NET MAUI. It should be noted that a Context Factory was used
for creating the backend. This full-stack application features 5 models: Client, Project, Employee, Time, and Bill.
Users may apply CRUD to each of these models (except for, of course, editing a bill). Clients and Projects may be closed
(this is used to exemplify closing a an active case or client to employee interaction). To close a client, all projects
must first be closed. Time entries can be recorded manually or by using the timer popup for a project (this timer limits
entries to greater than 10 minutes). Time entries must include a valid employee id, project id, and time. Users may undo
any changes during the process of editing or adding a Client, Project, Employee, Time, or Bill. Delete features chain deletion
(for example, deleting an employee deletes time entries associated with that employee and bills associated with those
time entries). Once a time entry has been billed, it can no longer be edited. Bills can be made for an individual project
or for a client as a whole. 
###### *Please note project was made in year 2023, not 2022 as indicated by the title.

Demonstration Video:
====================
[![](https://drive.google.com/uc?export=view&id=1n8E_2TCdd0EHYv17rBEpMFX4gWJZSW5_)](https://youtu.be/4RhOKUSKGfs)
