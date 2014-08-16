#!/usr/bin/perl
my $version = `/usr/libexec/PlistBuddy -c "Print :CFBundleShortVersionString" "Info.plist"`;
if ($version =~ /(\d+)\.(\d+)\.(\d+)/) {
    my $major = int($1);
    my $minor = int($2);
    my $rev   = int($3) + 1;
    my $result = `/usr/libexec/PlistBuddy -c "Set :CFBundleShortVersionString $major.$minor.$rev" "Info.plist"`;
}