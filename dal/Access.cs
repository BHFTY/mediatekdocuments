﻿using System;
using System.Collections.Generic;
using MediaTekDocuments.model;
using MediaTekDocuments.manager;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Configuration;

namespace MediaTekDocuments.dal
{
    /// <summary>
    /// Classe d'accès aux données
    /// </summary>
    public class Access
    {
        /// <summary>
        /// adresse de l'API
        /// </summary>
        private static readonly string uriApi = "http://localhost/rest_mediatekdocuments/";
        /// <summary>
        /// instance unique de la classe
        /// </summary>
        private static Access instance = null;
        /// <summary>
        /// instance de ApiRest pour envoyer des demandes vers l'api et recevoir la réponse
        /// </summary>
        private readonly ApiRest api = null;
        /// <summary>
        /// méthode HTTP pour select
        /// </summary>
        private const string GET = "GET";
        /// <summary>
        /// méthode HTTP pour insert
        /// </summary>
        private const string POST = "POST";
        /// <summary>
        /// méthode HTTP pour update
        /// </summary>
        private const string PUT = "PUT";
        /// <summary>
        /// méthode HTTP pour delete
        /// </summary>
        private const string DELETE = "DELETE";


        /// <summary>
        /// Méthode privée pour créer un singleton
        /// initialise l'accès à l'API
        /// </summary>
        private Access()
        {
            String authenticationString;
            try
            {
                authenticationString = "admin:adminpwd";
                api = ApiRest.GetInstance(uriApi, authenticationString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Création et retour de l'instance unique de la classe
        /// </summary>
        /// <returns>instance unique de la classe</returns>
        public static Access GetInstance()
        {
            if(instance == null)
            {
                instance = new Access();
            }
            return instance;
        }

        /// <summary>
        /// Retourne tous les genres à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Genre</returns>
        public List<Categorie> GetAllGenres()
        {
            IEnumerable<Genre> lesGenres = TraitementRecup<Genre>(GET, "genre");
            return new List<Categorie>(lesGenres);
        }

        /// <summary>
        /// Retourne tous les rayons à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Rayon</returns>
        public List<Categorie> GetAllRayons()
        {
            IEnumerable<Rayon> lesRayons = TraitementRecup<Rayon>(GET, "rayon");
            return new List<Categorie>(lesRayons);
        }

        /// <summary>
        /// Retourne toutes les catégories de public à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Public</returns>
        public List<Categorie> GetAllPublics()
        {
            IEnumerable<Public> lesPublics = TraitementRecup<Public>(GET, "public");
            return new List<Categorie>(lesPublics);
        }

        /// <summary>
        /// Retourne toutes les livres à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Livre</returns>
        public List<Livre> GetAllLivres()
        {
            List<Livre> lesLivres = TraitementRecup<Livre>(GET, "livre");
            return lesLivres;
        }

        /// <summary>
        /// Ajouter un document dans la BDD, return true si l'opération est correcte
        /// </summary>
        /// <param name="jsonDocument"></param>
        /// <returns></returns>
        public bool CreerDocument(String jsonDocument)
        {
            jsonDocument = jsonDocument.Replace(' ', '-');
            try
            {
                List<Object> liste = TraitementRecup<Object>(POST, "document/" + jsonDocument);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Modifie un document dans la BDD, return true si l'opération est correcte
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jsonDocument"></param>
        /// <returns></returns>
        public bool UpdateDocument(string id, String jsonDocument)
        {
            jsonDocument = jsonDocument.Replace(' ', '-');
            try
            {
                List<Object> liste = TraitementRecup<Object>(PUT, "document/" + id + "/" + jsonDocument);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Supprime un document dans la BDD, return true si l'opération est correcte
        /// </summary>
        /// <param name="jsonDocument"></param>
        /// <returns></returns>
        public bool SupprimerDocument(String jsonDocument)
        {
            jsonDocument = jsonDocument.Replace(' ', '-');
            try
            {
                List<Object> liste = TraitementRecup<Object>(DELETE, "document/" + jsonDocument);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Ajouter un livre_dvd dans la BDD, return true si l'opération est correct
        /// </summary>
        /// <param name="jsonLivreDvd"></param>
        /// <returns></returns>
        public bool CreerLivreDvd(String jsonLivreDvd)
        {
            try
            {
                List<Object> liste = TraitementRecup<Object>(POST, "livres_dvd/" + jsonLivreDvd);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Supprime un livre_dvd dans la BDD, return true si l'opération est correcte
        /// </summary>
        /// <param name="jsonLivreDvd"></param>
        /// <returns></returns>
        public bool SupprimerLivreDvD(String jsonLivreDvd)
        {
            jsonLivreDvd = jsonLivreDvd.Replace(' ', '-');
            try
            {
                List<Object> liste = TraitementRecup<Object>(DELETE, "livres_dvd/" + jsonLivreDvd);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Ajouter un livre dans la BDD, return true si l'opération est correcte
        /// </summary>
        /// <param name="jsonLivre"></param>
        /// <returns></returns>
        public bool CreerLivre(String jsonLivre)
        {
            jsonLivre = jsonLivre.Replace(' ', '-');
            try
            {
                List<Object> liste = TraitementRecup<Object>(POST, "livre/" + jsonLivre);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Modifie un livre dans la BDD, return true si l'opération est correcte
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jsonLivre"></param>
        /// <returns></returns>
        public bool UpdateLivre(string id, String jsonLivre)
        {
            jsonLivre = jsonLivre.Replace(' ', '-');
            try
            {
                List<Object> liste = TraitementRecup<Object>(PUT, "livre/" + id + "/" + jsonLivre);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Supprime un livre dans la BDD, return true si l'opération est correcte
        /// </summary>
        /// <param name="jsonLivreDvd"></param>
        /// <returns></returns>
        public bool SupprimerLivre(String jsonLivreDvd)
        {
            jsonLivreDvd = jsonLivreDvd.Replace(' ', '-');
            try
            {
                List<Object> liste = TraitementRecup<Object>(DELETE, "livre/" + jsonLivreDvd);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Créer un DVD dans la BDD, return true si l'opération est correcte
        /// </summary>
        /// <param name="jsonDvd"></param>
        /// <returns></returns>
        public bool CreerDvd(String jsonDvd)
        {
            jsonDvd = jsonDvd.Replace(' ', '-');
            try
            {
                List<Object> liste = TraitementRecup<Object>(POST, "dvd/" + jsonDvd);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Modifie un DVD dans la BDD, return true si l'opération est correcte
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jsonDvd"></param>
        /// <returns></returns>
        public bool UpdateDvd(string id, String jsonDvd)
        {
            jsonDvd = jsonDvd.Replace(' ', '-');
            try
            {
                List<Object> liste = TraitementRecup<Object>(PUT, "dvd/" + id + "/" + jsonDvd);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Supprime un DVD dans la BDD, return true si l'opération est correcte
        /// </summary>
        /// <param name="jsonDvd"></param>
        /// <returns></returns>
        public bool SupprimerDvd(String jsonDvd)
        {
            jsonDvd = jsonDvd.Replace(' ', '-');
            try
            {
                List<Object> liste = TraitementRecup<Object>(DELETE, "dvd/" + jsonDvd);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Ajouter une revue dans la BDD, return true si l'opération est correcte
        /// </summary>
        /// <param name="jsonRevue"></param>
        /// <returns></returns>
        public bool CreerRevue(String jsonRevue)
        {
            jsonRevue = jsonRevue.Replace(' ', '-');
            try
            {
                List<Object> liste = TraitementRecup<Object>(POST, "revue/" + jsonRevue);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Modifie une revue dans la BDD, return true si l'opération est correcte
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jsonRevue"></param>
        /// <returns></returns>
        public bool UpdateRevue(string id, String jsonRevue)
        {
            jsonRevue = jsonRevue.Replace(' ', '-');
            try
            {
                List<Object> liste = TraitementRecup<Object>(PUT, "revue/" + id + "/" + jsonRevue);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Supprime une revue dans la BDD, return true si l'opération est correcte
        /// </summary>
        /// <param name="jsonRevue"></param>
        /// <returns></returns>
        public bool SupprimerRevue(String jsonRevue)
        {
            jsonRevue = jsonRevue.Replace(' ', '-');
            try
            {
                List<Object> liste = TraitementRecup<Object>(DELETE, "revue/" + jsonRevue);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }


        /// <summary>
        /// Retourne toutes les dvd à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Dvd</returns>
        public List<Dvd> GetAllDvd()
        {
            List<Dvd> lesDvd = TraitementRecup<Dvd>(GET, "dvd");
            return lesDvd;
        }

        /// <summary>
        /// Retourne toutes les revues à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Revue</returns>
        public List<Revue> GetAllRevues()
        {
            List<Revue> lesRevues = TraitementRecup<Revue>(GET, "revue");
            return lesRevues;
        }


        /// <summary>
        /// Retourne les exemplaires d'une revue
        /// </summary>
        /// <param name="idDocument">id de la revue concernée</param>
        /// <returns>Liste d'objets Exemplaire</returns>
        public List<Exemplaire> GetExemplairesRevue(string idDocument)
        {
            String jsonIdDocument = convertToJson("id", idDocument);
            List<Exemplaire> lesExemplaires = TraitementRecup<Exemplaire>(GET, "exemplaire/" + jsonIdDocument);
            return lesExemplaires;
        }

        /// <summary>
        /// ecriture d'un exemplaire en base de données
        /// </summary>
        /// <param name="exemplaire">exemplaire à insérer</param>
        /// <returns>true si l'insertion a pu se faire (retour != null)</returns>
        public bool CreerExemplaire(Exemplaire exemplaire)
        {
            String jsonExemplaire = JsonConvert.SerializeObject(exemplaire, new CustomDateTimeConverter());
            try {
                // récupération soit d'une liste vide (requête ok) soit de null (erreur)
                List<Exemplaire> liste = TraitementRecup<Exemplaire>(POST, "exemplaire/" + jsonExemplaire);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false; 
        }

        /// <summary>
        /// Traitement de la récupération du retour de l'api, avec conversion du json en liste pour les select (GET)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methode">verbe HTTP (GET, POST, PUT, DELETE)</param>
        /// <param name="message">information envoyée</param>
        /// <returns>liste d'objets récupérés (ou liste vide)</returns>
        private List<T> TraitementRecup<T> (String methode, String message)
        {
            List<T> liste = new List<T>();
            try
            {
                Console.WriteLine("TraitementRecup string = " + message);
                JObject retour = api.RecupDistant(methode, message);
                String code = (String)retour["code"];
                if (code.Equals("200"))
                {
                    if (methode.Equals(GET))
                    {
                        String resultString = JsonConvert.SerializeObject(retour["result"]);
                        liste = JsonConvert.DeserializeObject<List<T>>(resultString, new CustomBooleanJsonConverter());
                    }
                }
                else
                {
                    Console.WriteLine("code erreur = " + code + " message = " + (String)retour["message"]);
                }
            }catch(Exception e)
            {
                Console.WriteLine("Erreur lors de l'accès à l'API : "+e.Message);
                Environment.Exit(0);
            }
            return liste;
        }

        /// <summary>
        /// Convertit en json un couple nom/valeur
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="valeur"></param>
        /// <returns>couple au format json</returns>
        private String convertToJson(Object nom, Object valeur)
        {
            Dictionary<Object, Object> dictionary = new Dictionary<Object, Object>();
            dictionary.Add(nom, valeur);
            return JsonConvert.SerializeObject(dictionary);
        }

        /// <summary>
        /// Modification du convertisseur Json 
        /// </summary>
        private sealed class CustomDateTimeConverter : IsoDateTimeConverter
        {
            public CustomDateTimeConverter()
            {
                base.DateTimeFormat = "yyyy-MM-dd";
            }
        }

        /// <summary>
        /// Modification du convertisseur Json pour prendre en compte les booléens
        /// classe trouvée sur le site :
        /// https://www.thecodebuzz.com/newtonsoft-jsonreaderexception-could-not-convert-string-to-boolean/
        /// </summary>
        private sealed class CustomBooleanJsonConverter : JsonConverter<bool>
        {
            public override bool ReadJson(JsonReader reader, Type objectType, bool existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                return Convert.ToBoolean(reader.ValueType == typeof(string) ? Convert.ToByte(reader.Value) : reader.Value);
            }

            public override void WriteJson(JsonWriter writer, bool value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

    }
}
