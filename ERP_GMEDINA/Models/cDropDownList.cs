namespace ERP_GMEDINA.Models
{
    public class cDropDownList
    {
        public cDropDownList()
        {
            Id = 0;
            Descripcion = "**Se produjo un error en la conexion**";
        }
        public cDropDownList(int Id,string Descripcion)
        {
            this.Id = Id;
            this.Descripcion = Descripcion;
        }
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }
}