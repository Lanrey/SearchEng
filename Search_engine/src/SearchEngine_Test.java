import java.io.IOException;

/**
 * Created by SAMBOYE 2 on 27/06/2016.
 */
public class SearchEngine_Test
{
    public static void main(String[] args) throws IOException {
        Inverted_Index tester = new Inverted_Index();

        tester.Readfile();
        tester.TokenizedWords();
    }
}
