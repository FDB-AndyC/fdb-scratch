public class ActiveSwitchParser {

    public static ActiveSwitch Parse(String s) {
        if (s == null) {
            return ActiveSwitch.Default;
        }

        if (s.length() < 1) {
            return ActiveSwitch.Default;
        }

        if (s.startsWith("-") || s.startsWith("/")) {
            String switchName = s.substring(1);
            switch (switchName.toLowerCase()) {
                case "list":
                case "roots":
                    return ActiveSwitch.ListTrustedRoots;
            }
        }

        return ActiveSwitch.Default;
    }
}
