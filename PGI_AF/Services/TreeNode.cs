using BackEnd_PGI.Model;
using BlazorBootstrap;

namespace PGI_AF.Services
{
    public class TreeNode
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public string Tipo { get; set; }
        public IconName Icon { get; set; }
        public List<TreeNode> Children { get; set; }

        public TreeNode()
        {
            Children = new List<TreeNode>();            
        }
    }


}
