namespace IISTestApplication.Models.MapperModels
{
    public class Model1
    {
        public int Property1 { get; set; }

        private int Property2 { get; set; }

         int Property3 { get; set; }

        public int property4;

        private int property5;

        protected int property6;

        public Model1()
        {

        }

        public Model1(int p1, int p2, int p3, int p4, int p5, int p6)
        {
            Property1 = p1;
            Property2 = p2;
            Property3 = p3;
            property4 = p4;
            property5 = p5;
            property6 = p6;
        }
    }

    public class Model2 : Model1
    {
        public Model2()
        {

        }

        public Model2(int p1, int p2, int p3, int p4, int p5, int p6) : base(p1, p2, p3, p4, p5, p6)
        {

        }
    }
}
