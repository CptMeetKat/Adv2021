public abstract class AocMachine 
{
   protected List<string> data;
   protected string filename;
   protected int result;


   public AocMachine(string filename)
   {
      data = new List<string>();
      this.filename = filename;
   }

   

   protected void readData(string filename)
   {
      string[] fileData = File.ReadAllLines(filename);
      foreach (string s in fileData)
      {
         data.Add(s);
      }
   }

   public abstract void run(); 
   public abstract void displayResults();
}