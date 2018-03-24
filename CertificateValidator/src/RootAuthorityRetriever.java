import javax.net.ssl.TrustManagerFactory;
import javax.net.ssl.X509TrustManager;
import java.security.KeyStore;
import java.security.KeyStoreException;
import java.security.NoSuchAlgorithmException;
import java.security.cert.Certificate;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class RootAuthorityRetriever {
    public static String[] listAuthorities() throws NoSuchAlgorithmException, KeyStoreException {
        TrustManagerFactory trustManagerFactory =
                TrustManagerFactory.getInstance(TrustManagerFactory.getDefaultAlgorithm());
        List<Certificate> x509Certificates = new ArrayList<>();
        trustManagerFactory.init((KeyStore)null);
        Arrays.asList(trustManagerFactory.getTrustManagers()).stream().forEach(t -> x509Certificates.addAll(Arrays.asList(((X509TrustManager)t).getAcceptedIssuers())));

        return x509Certificates.forEach(c -> c.toString());
    }
}
