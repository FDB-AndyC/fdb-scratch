import java.net.MalformedURLException;
import java.security.KeyStoreException;
import java.security.NoSuchAlgorithmException;

public class Main {

    public static void main(String[] args) {

        for (int argc = 0; argc < args.length; argc++) {
            String arg = args[argc];
            ActiveSwitch mode = ActiveSwitchParser.Parse(args[argc]);

            switch(mode) {
                case ListTrustedRoots:
                    listRootCAs();
                    break;

                default:
                    validateUrl(arg);
                    break;
            }

        }
    }

    private static void listRootCAs() {

        try {
            String[] authorities = RootAuthorityRetriever.listAuthorities();

            for (int i = 0; i < authorities.length; ++i) {
                System.out.println("Root CA:" + authorities[i]);
            }
        } catch (NoSuchAlgorithmException e) {
            System.err.println("Error retrieving CA roots (no such algorithm): ");
            e.printStackTrace();
        } catch (KeyStoreException e) {
            System.err.println("Error retrieving CA roots (key store exception): ");
            e.printStackTrace();
        } catch (Exception e) {
            System.err.println("Error retrieving CA roots (exception): ");
            e.printStackTrace();
        }
    }

    private static void validateUrl(String url) {
        CertificateValidator validator = new CertificateValidator();

        try {
            validator.testConnectionTo(url);
            System.out.println(url + ": OK");
        } catch (MalformedURLException e) {
            System.err.println(url + " caused malformed URL exception: ");
            e.printStackTrace();
        } catch (Exception e) {
            System.err.println(url + " caused exception: ");
            e.printStackTrace();
        }
    }
}
