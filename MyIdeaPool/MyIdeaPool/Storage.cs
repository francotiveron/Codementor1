﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyIdeaPool.Models;

namespace MyIdeaPool
{
    internal class Storage
    {
        private static Dictionary<string, User> users = new Dictionary<string, User>();
        private static Dictionary<string, Jwt.Token> tokens = new Dictionary<string, Jwt.Token>();
        private static Dictionary<string, Dictionary<string, Idea>> ideas = new Dictionary<string, Dictionary<string, Idea>>();

        public static void PutUser(User user)
        {
            users[user.email] = user;
        }
        public static User GetUser(string email)
        {
            User user;
            if (users.TryGetValue(email, out user)) return user;

            return null;
        }
        public static void PutToken(Jwt.Token token)
        {
            tokens[token.refresh_token] = token;
        }
        public static bool UserExists(string jwt)
        {
            return tokens.Any(kvp => kvp.Value.jwt == jwt);
        }
        public static Jwt.Token GetToken(string refresh_token)
        {
            return tokens[refresh_token];
        }
        public static bool RemoveToken(string refresh_token)
        {
            tokens.Remove(refresh_token);
            return true;
        }
        public static void PutIdea(Idea idea, string email)
        {
            IdeasOf(email)[idea.id] = idea;
        }
        public static bool RemoveIdea(string id, string email)
        {
            IdeasOf(email).Remove(id);
            return true;
        }
        public static List<Idea> GetIdeas(string email)
        {
            return IdeasOf(email).Values.Take(10).ToList();
        }
        public static bool IdeaExists(string id, string email)
        {
            return IdeasOf(email).ContainsKey(id);
        }
        private static Dictionary<string, Idea> IdeasOf(string email)
        {
            Dictionary<string, Idea> ideasOf;
            if (!ideas.TryGetValue(email, out ideasOf))
            {
                ideasOf = new Dictionary<string, Idea>();
                ideas[email] = ideasOf;
            }
            return ideasOf;
        }
    }
}