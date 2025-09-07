using System.ComponentModel.DataAnnotations;
using TpCommandManagerDbContext.Entities;

namespace TpCommandManagerMain.Models;

public class ProduitCommandeModel
{
    public int Id { get; set; }
    public Commande Commande { get; set; }

    public Produit Produit { get; set; } = null!;


    public ProduitCommandeModel(int id, Commande Commande, Produit produit)
    {
        this.Id = id;
        this.Commande = Commande;
        this.Produit = produit;
    }
}