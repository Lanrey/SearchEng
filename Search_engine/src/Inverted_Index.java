import java.io.*;
import java.util.ArrayList;


/**
 * Created by SAMBOYE 2 on 26/06/2016.
 */

/**
 * Class implements the inverted index for the Search Engine
 * Steps:
 * 1. Collect the documents to be indexed
 * 2. Tokenize the text, turning each document into a list of tokens
 * 3. Normalize the tokens (i.e. remove stops words)
 * 4. Map each token to the posting lists
 */
public class Inverted_Index
{
    String path = "C:\\Users\\SAMBOYE 2\\Desktop\\Search";
    File documents = new File(path);
    FileReader reader;

    public ArrayList<String>  getPaths()
    {
        File [] ListofFiles = documents.listFiles();
        ArrayList<String>name = new ArrayList<>();

        for(File file: ListofFiles)
        {
            if(file.isFile())
            {
                name.add(file.getAbsolutePath());
            }
        }
        return name;
    }


    //Implements step 1.//
    //Ensures there are files in the path
    //Returns a list of files to be opened

    public ArrayList<String> Readfile() throws IOException {
        ArrayList<String> addWord = new ArrayList<>();
        String words;
        for(String check : getPaths())
        {
            reader = new FileReader(check);
            BufferedReader read = new BufferedReader(reader);

            while ((words = read.readLine()) != null) {
                addWord.add(words);
            }

            reader.close();
        }


        return addWord;
    }

    public ArrayList<String> TokenizedWords() throws IOException {

        ArrayList<String> tokenizedWords = new ArrayList<>();

        for (String tokens: Readfile())
        {
           String [] tokenArray = tokens.split(" ");

            for (String token : tokenArray)
                tokenizedWords.add(token);


        }
        System.out.println(tokenizedWords);

        return tokenizedWords;
    }
}
