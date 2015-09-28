using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace IdSrvHost
{
    public static class InMemoryConfig
    {
        public static IEnumerable<Client> Clients
        {
            get
            {
                return new[]
                {
                    new Client
                    {
                        ClientName = "App1",
                        ClientId = "app1_client_id",
                        ClientSecrets = new List<Secret>
                        {
                            new Secret("app1_client_secret")
                        },
                        Flow = Flows.Implicit,
                        AllowedScopes = new List<string> {"openid","profile","email"},

                        RedirectUris = new List<string>
                        {
                            "https://app1.example.com/callback/oidc",
                        },
                    },
                    new Client
                    {
                        ClientName = "App2",
                        ClientId = "app2_client_id",
                        Flow = Flows.Implicit,
                        AllowedScopes = new List<string> {"openid","resource","email"},

                        RedirectUris = new List<string>
                        {
                            "https://app2.example.com/callback.html",
                            "https://app2.example.com/dialog.html",
                        },
                    },
                };
            }
        }

        public static List<InMemoryUser> Users
        {
            get
            {
                return new List<InMemoryUser>
                {
                    new InMemoryUser
                    {
                        Subject = "1",
                        Username = "Alice",
                        Claims = new []
                        {
                            new Claim ("email", "alice4demos@gmail.com"),
                            new Claim ("name", "Alice")
                        },
                        Enabled = true,
                        Password = "1234",
                    }
                };
            }
        }

        public static IEnumerable<Scope> Scopes
        {
            get
            {
                return StandardScopes.All.Concat(new[]
                {
                    new Scope
                    {
                        Name = "resource",
                        DisplayName = "Allow some resource access",
                        Claims = new List<ScopeClaim>
                        {
                            new ScopeClaim("email", true),
                        }
                    }
                });
            }
        }
    }
}