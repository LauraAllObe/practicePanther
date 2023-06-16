using Summer2022Proj0.Models;
using System;
using System.ComponentModel.Design;
using System.Xml.Linq;
using Summer2022Proj0.library.Services;

namespace Summer2022Proj0.Models // Note: actual namespace depends on the project name.
{
    public class Program
    {
        static void Main(string[] args)
        {
            var clients = new List<Client>();
            var projects = new List<Project>();
            Menu(clients, projects);
        }

        //05.26 added for notes
        static void ProjectsMenu(List<Project> courses)
        {
            var myClientService = ClientService.Current;
        }

        static void Menu(List<Client> clients, List<Project> projects)
        {
            string ClientOrProjectChoice;
            string ClientOrProjectChoice;
            string CRUDChoice;
            int ClientID;
            DateTime ClientOpenDate;
            DateTime ClientClosedDate;
            Boolean ClientIsActive;
            String ClientName;
            String ClientNotes;
            int ProjectID;
            DateTime ProjectOpenDate;
            DateTime ProjectClosedDate;
            Boolean ProjectIsActive;
            String ProjectShortName;
            String ProjectLongName;
            int ProjectLinkedID;
            int ClientIdTotalAdded = 0;
            int ProjectIdTotalAdded = 0;
            do
            {
                Console.Write("Menu:\na. Add\nb. List\nc. Edit\nd. Delete\ne. Exit\n\nMenu Choice --> ");
                CRUDChoice = Console.ReadLine() ?? string.Empty;
                if (CRUDChoice.Equals("a", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.Write("\nSelect:\na. Add Client\nb. Add Project\n\nSelection Choice --> ");
                    ClientOrProjectChoice = Console.ReadLine() ?? string.Empty;
                    if (ClientOrProjectChoice.Equals("a", StringComparison.InvariantCultureIgnoreCase))
                    {
                        ClientIdTotalAdded++;
                        Console.Write("Input open date --> ");
                        DateTime dateValue;
                        string tryparsingfirst = Console.ReadLine() ?? string.Empty;
                        if (DateTime.TryParse(tryparsingfirst, out dateValue))
                            ClientOpenDate = DateTime.Parse(tryparsingfirst);
                        else
                            ClientOpenDate = DateTime.Today;
                        Console.Write("Input closed date --> ");
                        tryparsingfirst = Console.ReadLine() ?? string.Empty;
                        if (DateTime.TryParse(tryparsingfirst, out dateValue))
                            ClientClosedDate = DateTime.Parse(tryparsingfirst);
                        else
                            ClientClosedDate = DateTime.Today;
                        Console.Write("Is client active (true/false) --> ");
                        ClientIsActive = Boolean.Parse(Console.ReadLine() ?? Boolean.TrueString.ToString());
                        Console.Write("Input client name --> ");
                        ClientName = Console.ReadLine() ?? "John/Jane Doe";
                        Console.Write("Input notes --> ");
                        ClientNotes = Console.ReadLine() ?? "No Notes";

                        clients.Add(
                            new Client
                            {
                                Id = ClientIdTotalAdded,
                                OpenDate = ClientOpenDate,
                                ClosedDate = ClientClosedDate,
                                IsActive = ClientIsActive,
                                Name = ClientName,
                                Notes = ClientNotes
                            }
                        );
                    }
                    else if (ClientOrProjectChoice.Equals("b", StringComparison.InvariantCultureIgnoreCase))
                    {
                        ProjectIdTotalAdded++;
                        Console.Write("Input open date --> ");
                        DateTime dateValue;
                        string tryparsingfirst = Console.ReadLine() ?? string.Empty;
                        if (DateTime.TryParse(tryparsingfirst, out dateValue))
                            ProjectOpenDate = DateTime.Parse(tryparsingfirst);
                        else
                            ProjectOpenDate = DateTime.Today;
                        Console.Write("Input closed date --> ");
                        tryparsingfirst = Console.ReadLine() ?? string.Empty;
                        if (DateTime.TryParse(tryparsingfirst, out dateValue))
                            ProjectClosedDate = DateTime.Parse(tryparsingfirst);
                        else
                            ProjectClosedDate = DateTime.Today;
                        Console.Write("Is project active (true/false) --> ");
                        ProjectIsActive = Boolean.Parse(Console.ReadLine() ?? Boolean.TrueString.ToString());
                        Console.Write("Input short name --> ");
                        ProjectShortName = Console.ReadLine() ?? "John/Jane";
                        Console.Write("Input long name --> ");
                        ProjectLongName = Console.ReadLine() ?? "Doe";
                        Console.Write("Input client ID --> ");
                        ProjectLinkedID = int.Parse(Console.ReadLine() ?? "0");

                        Boolean isLinked = false;
                        foreach (var client in clients)
                        {
                            if (client.Id == ProjectLinkedID)
                            {
                                isLinked = true;
                                break;
                            }

                        }
                        if (isLinked == false)
                        {
                            ProjectLinkedID = 0;
                            Console.WriteLine("No such client exists, assigning default value.");
                        }

                        projects.Add(
                            new Project
                            {
                                Id = ProjectIdTotalAdded,
                                OpenDate = ProjectOpenDate,
                                ClosedDate = ProjectClosedDate,
                                IsActive = ProjectIsActive,
                                ShortName = ProjectShortName,
                                LongName = ProjectLongName,
                                ClientId = ProjectLinkedID
                            }
                        );
                    }
                    else
                    {
                        Console.WriteLine("Sorry, that functionality isn't supported");
                    }
                }
                else if (CRUDChoice.Equals("b", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.Write("\nSelect:\na. List Clients\nb. List Projects\nc. Linked Listing\n\nSelection Choice --> ");
                    ClientOrProjectChoice = Console.ReadLine() ?? string.Empty;
                    if (ClientOrProjectChoice.Equals("a", StringComparison.InvariantCultureIgnoreCase))
                    {
                        clients.ForEach(Console.WriteLine);
                    }
                    else if (ClientOrProjectChoice.Equals("b", StringComparison.InvariantCultureIgnoreCase))
                    {
                        projects.ForEach(Console.WriteLine);
                    }
                    else if (ClientOrProjectChoice.Equals("c", StringComparison.InvariantCultureIgnoreCase))
                    {
                        foreach (var client in clients)
                        {
                            Console.WriteLine(client);
                            foreach (var project in projects)
                            {
                                if (project.ClientId == client.Id)
                                    Console.WriteLine("\t" + project);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Sorry, that functionality isn't supported");
                    }
                }
                else if (CRUDChoice.Equals("c", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.Write("\nSelect:\na. Edit Client\nb. Edit Project\n\nSelection Choice --> ");
                    ClientOrProjectChoice = Console.ReadLine() ?? string.Empty;
                    if (ClientOrProjectChoice.Equals("a", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Console.WriteLine("Which client should be updated?");
                        clients.ForEach(Console.WriteLine);
                        Console.Write("Input client ID to update --> ");
                        var updateChoice = int.Parse(Console.ReadLine() ?? "0");

                        var clientToUpdate = clients.FirstOrDefault(s => s.Id == updateChoice);
                        if (clientToUpdate != null)
                        {
                            Console.Write("What are the client's updated notes? --> ");
                            clientToUpdate.Notes = Console.ReadLine() ?? "N/A";
                        }
                    }
                    else if (ClientOrProjectChoice.Equals("b", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Console.WriteLine("Which project should be updated?");
                        projects.ForEach(Console.WriteLine);
                        Console.Write("Input project ID to update --> ");
                        var updateChoice = int.Parse(Console.ReadLine() ?? "0");

                        var projectToUpdate = projects.FirstOrDefault(s => s.Id == updateChoice);
                        if (projectToUpdate != null)
                        {
                            Console.Write("What are the project's updated short name? --> ");
                            projectToUpdate.ShortName = Console.ReadLine() ?? "shortName";
                        }
                        Console.Write("Update client (linked) id? (y/n) --> ");
                        string updateLinked = Console.ReadLine() ?? string.Empty;
                        if (updateLinked.Equals("y", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Console.Write("Input client ID --> ");
                            int ProjectLinkedId = int.Parse(Console.ReadLine() ?? "0");

                            Boolean isLinked = false;
                            foreach (var client in clients)
                            {
                                if (client.Id == ProjectLinkedId)
                                {
                                    isLinked = true;
                                    projectToUpdate.ClientId = ProjectLinkedId;
                                    break;
                                }
                            }
                            if (isLinked == false)
                            {
                                ProjectLinkedId = 0;
                                Console.WriteLine("No such client exists, keeping previous value.");
                            }
                        }
                        else if (updateLinked.Equals("n", StringComparison.InvariantCultureIgnoreCase))
                        {
                            break;
                        }
                        else
                            Console.WriteLine("Sorry, that functionality isn't supported");
                    }
                    else
                    {
                        Console.WriteLine("Sorry, that functionality isn't supported");
                    }
                }
                else if (CRUDChoice.Equals("d", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.Write("\nSelect:\na. Delete Client\nb. Delete Project\n\nSelection Choice --> ");
                    ClientOrProjectChoice = Console.ReadLine() ?? string.Empty;
                    if (ClientOrProjectChoice.Equals("a", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Console.WriteLine("Which client should be deleted?");
                        clients.ForEach(Console.WriteLine);
                        Console.Write("Input client ID to delete --> ");
                        var deleteChoice = int.Parse(Console.ReadLine() ?? "0");

                        var clientToRemove = clients.FirstOrDefault(s => s.Id == deleteChoice);
                        if (clientToRemove != null)
                        {
                            clients.Remove(clientToRemove);
                        }
                    }
                    else if (ClientOrProjectChoice.Equals("b", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Console.WriteLine("Which project should be deleted?");
                        projects.ForEach(Console.WriteLine);
                        Console.Write("Input project ID to delete --> ");
                        var deleteChoice = int.Parse(Console.ReadLine() ?? "0");

                        var projectToRemove = projects.FirstOrDefault(s => s.Id == deleteChoice);
                        if (projectToRemove != null)
                        {
                            projects.Remove(projectToRemove);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Sorry, that functionality isn't supported");
                    }

                }
                else if (CRUDChoice.Equals("e", StringComparison.InvariantCultureIgnoreCase))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Sorry, that functionality isn't supported");
                }
                Console.WriteLine("");
            } while (!(CRUDChoice.Equals("e", StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}