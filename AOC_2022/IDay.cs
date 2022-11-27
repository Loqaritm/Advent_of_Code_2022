namespace AOC_2022 {
    public interface IDay<T>
    {
        // TODO:
        // Need to have a method to load data
        // Need to have a run method that takes this data
        // Should the data loader be a separate Interface?
        // Would it be wrong to have a Test() method that makes sure it works as expected and I can have this in a single file?
        // Lets implement this along with the first task I guess
        // And I will learn a bit of c# at the same time
        // Though I won't be surprised if I make some of the programs in other languages - gotta learn how to be a swift mobile dev someday


        public T Run();
        
    }
}