import javax.net.ssl.HttpsURLConnection;
import java.net.URL;
import java.security.cert.Certificate;
import java.security.cert.X509Certificate;

public class CertificateValidator {

    public CertificateValidator() {
        super();
    }

    public void testConnectionTo(String aURL) throws Exception {
        URL destinationURL = new URL(aURL);
        HttpsURLConnection conn = (HttpsURLConnection) destinationURL
                .openConnection();
        conn.connect();
        Certificate[] certs = conn.getServerCertificates();
        for (Certificate cert : certs) {
            if(cert instanceof X509Certificate) {
                X509Certificate x509cert = (X509Certificate) cert;
                x509cert.checkValidity();
            }
        }
    }
}