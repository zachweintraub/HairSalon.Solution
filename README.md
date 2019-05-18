# Hair Salon Admin Console

#### An ASP.NET Core MVC app to create records for stylists, clients, and appointments in a hair salon.

#### By **Zach Weintraub**

## Description

A C# application that allows employees of a hair salon to create records for stylists, clients, and appointments in a hair salon.

| Specs |
| :-------------     |
|Salon employee can see a list of all stylists.|
|Salon employee can select a stylist, see their details, and see a list of all clients that belong to that stylist.|
|Salon employee can add new stylists to the system when they are hired.|
|Salon employee can add new clients to a specific stylist. They cannot add a client if no stylists have been added.|
|Salon employee can delete a client.|
|Salon employee can edit a client's information.|
|Salon employee can delete stylists.|
|Salon employee can delete clients.|
|Salon employee can view individual clients.|
|Salon employee can edit stylists.|
|Salon employee can edit clients.|
|Salon employee can assign multiple specialties to a stylist.|
|Salon employee can see a given stylist's specialties.|

## Setup/Installation Requirements

1. Clone this repository: $ git clone https:/github.com/zachweintraub/HairSalon.Solution.git
2. Change into the work directory:: $ cd HairSalon.Solution
3. To edit the project, open the project in your preferred text editor.
4. To connect the appropriate databases, start MAMP and click Open WebStart page in the MAMP window.
5. In the website you're taken to, select phpMyAdmin from the Tools dropdown.
6. Select the Import tab.
7. Select zach_weintraub.sql from the top level of the project's directory and click Go.
8. Repeat steps 6 and 7 with the file zach_weintraub_test.sql, also in the project directory's top level.
9. To run the program, first navigate to the HairSalon directory, then run the following commands: $ dotnet restore $ dotnet run, and open the resulting url in your browser of choice.

## Known Bugs
* No known bugs at this time.

## Technologies Used
* C# .NET Core App 2.2.103 & ASP.NETCore SQL MAMP Mono Atom Git Github

## Support and contact details

_Email me with any questions, comments, or concerns:_
zachweintraub@gmail.com

### License

*{This software is licensed under the MIT license}*

Copyright (c) 2019 **_{Zach Weintraub}_**